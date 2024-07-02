using RecordClique_BusinessLogic.DTOs;
using RecordClique_BusinessLogic.TokenAuthentication;
using RecordClique_DataAccess.Entities;
using System.Security.Claims;

namespace RecordClique_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<TokenApiDto> Authenticate(User userObj);
        Task<string> RegisterUser(User userObj);
        Task<bool> CheckUserNameExistAsync(string userName);
        Task<bool> CheckEmailExistAsync(string email);
        string CheckPasswordStrength(string password);
        string CreateJWT(User user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<string> CreateRefreshToken();
        Task<TokenApiDto> Refresh(TokenApiDto tokenApiDto);
        Task<object> SendEmail(string email);
        Task<object> ResetPassword(ResetPasswordDto resetPasswordDto);
        Task<UserDto> GetUserDetails(string username);
        Task<string> GetUserInitials(string username);
    }
}
