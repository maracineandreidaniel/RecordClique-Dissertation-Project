using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordClique_API.Services.Interfaces;
using RecordClique_BusinessLogic.Exceptions;
using RecordClique_BusinessLogic.TokenAuthentication;
using RecordClique_DataAccess.Entities;

namespace RecordClique_API.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }


        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            try
            {
                var result = await _userService.Authenticate(userObj);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (IncorrectPasswordException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            try
            {
                var result = await _userService.RegisterUser(userObj);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = result
                });
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (AlreadyExistsException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (IncorrectPasswordException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500,  $"An error occurred while saving the entity changes: {ex.InnerException?.Message}" );
            }
        }


        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenApiDto tokenApiDto)
        {
            try
            {
                var result = await _userService.Refresh(tokenApiDto);
                return Ok(result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (InvalidRequestException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch
            {
                return StatusCode(500, new { Message = "An unexpected error occurred." });
            }
        }


        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            try
            {
                var result = await _userService.SendEmail(email);
                return StatusCode(200, result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch 
            {
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            try
            {
                var result = await _userService.ResetPassword(resetPasswordDto);
                return StatusCode(200, result);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (InvalidRequestException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch (IncorrectPasswordException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("user-details")]
            public async Task<IActionResult> GetUserDetails(string username)
            {               

                try
                {
                    var userDetails = await _userService.GetUserDetails(username);
                    return Ok(userDetails);
                }
                catch (NotFoundException ex)
                {
                    return StatusCode(404, ex.Message);
            }
                catch
                {
                    return StatusCode(500, "An unexpected error occurred.");
                }
            }

        [HttpGet("user-initials")]
        public async Task<IActionResult> GetUserInitials(string username)
        {

            try
            {
                var userInitials = await _userService.GetUserInitials(username);
                return Ok(userInitials);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("links")]
        public async Task<IActionResult> GetUserAlbumLinks(int pageNumber, int pageSize, Guid? albumId, Guid? userId)
        {

            try
            {
                var links = await _userService.GetUserAlbumLinks(pageNumber, pageSize, albumId, userId);
                return Ok(links);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(404, ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
