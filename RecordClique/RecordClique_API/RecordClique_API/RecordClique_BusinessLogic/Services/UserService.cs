using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecordClique_API.Services.Interfaces;
using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.Exceptions;
using RecordClique_BusinessLogic.Services.Abstractions;
using RecordClique_BusinessLogic.TokenAuthentication;
using RecordClique_DataAccess.Entities;
using RecordClique_DataAccess.Repository.Abstraction;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace RecordClique_BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IEmailService emailService, IMapper mapper) {
            this._userRepository = userRepository;
            this._emailService = emailService;
            this._mapper = mapper;
        }

        public async Task<TokenApiDto> Authenticate(User userObj)
        {
            if (userObj == null)
            {
                throw new NotFoundException("User was not found!");
            }

            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();

            var user = usersList.FirstOrDefault(x => x.UserName == userObj.UserName);

            if (user == null)
            {
                throw new NotFoundException("User was not found!");
            }


            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
            {
                throw new IncorrectPasswordException("Password is incorrect!");
            }

            user.Token = CreateJWT(user);
            var newAccessToken = user.Token;
            var newRefreshToken = await CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);

            await _userRepository.UpdateAsync(user, user.Id);

            return new TokenApiDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }

        public async Task<string> RegisterUser(User userObj)
        {
            if (userObj == null)
            {
                throw new NotFoundException("User was not found!");
            }

            if (await CheckUserNameExistAsync(userObj.UserName))
            {
                throw new AlreadyExistsException("Username already exists!");
            }

            if (await CheckEmailExistAsync(userObj.Email))
            {
                throw new AlreadyExistsException("E-mail already exists!");
            }

            if (IsValidEmail(userObj.Email) == false)
            {
                throw new InvalidEmailException("E-mail has an incorrect structure!");
            }

            if (userObj.Password != userObj.ConfirmedPassword)
            {
                throw new NotMatchingPasswordsException("Passwords do not match!");
            }

            var pass = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(pass))
            {
                throw new IncorrectPasswordException(pass);
            }

            userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.ConfirmedPassword = PasswordHasher.HashPassword(userObj.ConfirmedPassword);
            userObj.Role = "User";
            userObj.Token = "";
            userObj.RefreshToken = "";

            await _userRepository.AddAsync(userObj);

            return ("User added successfully!");
            
        }

        public async Task<TokenApiDto> Refresh(TokenApiDto tokenApiDto)
        {
            if (string.IsNullOrEmpty(tokenApiDto.AccessToken) || string.IsNullOrEmpty(tokenApiDto.RefreshToken))
            {
                throw new NotFoundException("Tokens are missing or empty.");
            }

            if (tokenApiDto is null)
            {
                throw new InvalidRequestException("Invalid Client Request");
            }

            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();

            string AccessToken = tokenApiDto.AccessToken;
            string RefreshToken = tokenApiDto.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(AccessToken);
            var username = principal.Identity.Name;
            var user = usersList.FirstOrDefault(u => u.UserName == username);
            if (user is null || user.RefreshToken != RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new InvalidRequestException("Invalid Request");
            }
            var newAccessToken = CreateJWT(user);
            var newRefreshToken = await CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _userRepository.UpdateAsync(user, user.Id);
            return new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }

        public async Task<object> SendEmail(string email)
        {
            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();

            var user = usersList.FirstOrDefault(a => a.Email == email);
            if (user is null)
            {
                throw new NotFoundException("Email Does Not exist");
            }
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            var emailModel = new EmailDto(email, "Reset Password", EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);
            await _userRepository.UpdateAsync(user, user.Id);
            return new { Message = "Email Sent!" };

        }

        public async Task<bool> CheckUserNameExistAsync(string userName)
        {
            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();
            return usersList.Any(x => x.UserName == userName);
        }
    

        public async Task<bool> CheckEmailExistAsync(string email)
        {
            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();
            return usersList.Any(x => x.Email == email);
        }

        public string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if (password.Length < 8)
            {
                sb.Append("Minimum password length should be 8!"+Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                sb.Append("Password should be AlphaNumeric!"+Environment.NewLine);
            }
            if (!Regex.IsMatch(password, "[<,@,#,,%,(,{,},!,?]"))
            {
                sb.Append("Password should contain special characters "+Environment.NewLine);
            }

            return sb.ToString();
        }

        public string CreateJWT(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);

        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken); //out -> passed by reference, it will be modified by the method
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null  || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is invalid token");
            return principal;
        }

        public async Task<string> CreateRefreshToken()
        {
            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();

            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);
            var tokenInUser = usersList
                .Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return await CreateRefreshToken();
            }
            return refreshToken;
        }

        public async Task<object> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();

            if (resetPasswordDto.ConfirmPassword != resetPasswordDto.NewPassword)
            {
                throw new IncorrectPasswordException("Passwords does not match!");
            }
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = usersList.FirstOrDefault(a => a.Email == resetPasswordDto.Email);
            if (user is null)
            {
                throw new NotFoundException("Email does not exist!");
            }
            var tokenCode = user.ResetPasswordToken;
            DateTime? emailTokenExpiry = user.ResetPasswordExpiry;
            if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                throw new InvalidRequestException("Reset link is invalid!");
            }
            user.Password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            user.ConfirmedPassword = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            await _userRepository.UpdateAsync(user, user.Id);
            return new { Message = "Password Reset Successfullly" };
        }

        public async Task<UserDto> GetUserDetails (string username)
        {

            var usersQuerayble = await _userRepository.GetAll();
            var usersList = await usersQuerayble.ToListAsync();

            var userDetails = usersList
                .Where( u => u.UserName == username)
                .FirstOrDefault();
            if (userDetails is null)
            {
                throw new NotFoundException("User was not found!");
            }
            return _mapper.Map<UserDto>(userDetails);
        }

        public async Task<string> GetUserInitials (string username)
        {
            var user = await GetUserDetails (username);
            var userInitials = "" + user.FirstName[0] + user.LastName[0];
            return userInitials;
        }

        public bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

    }
}
