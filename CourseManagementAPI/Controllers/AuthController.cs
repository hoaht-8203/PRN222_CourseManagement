using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Model.ViewModel;
using System.Security.Claims;
using CourseManagement.Model.CustomResponses;
using CourseManagement.Business.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Microsoft.AspNetCore.Identity.Data;
using System.Net;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly MailService _mailService;
        private readonly IConfiguration _config;

        public AuthController(UserManager<AppUser> userManager, MailService mailService, IConfiguration config)
        {
            _userManager = userManager;
            _mailService = mailService;
            _config = config;
        }

        [HttpPost("register/v1")]
        public async Task<IActionResult> Register([FromBody] RegisterSdi req)
        {
            var userExists = await _userManager.FindByEmailAsync(req.Email);
            if (userExists != null) {
                return BadRequest(new { Errors = new[] { $"User {req.Email} already exists!" } });
            }

            var user = new AppUser {
                FullName = req.FullName,
                UserName = req.Email,
                Email = req.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, req.Password);
            if (!result.Succeeded) {
                return BadRequest(new { Errors = result.Errors.Select(p => p.Description) });
            }

            await _userManager.AddToRoleAsync(user, "User");

            var userCreated = await _userManager.FindByEmailAsync(req.Email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(userCreated);
            // Send verify code to client
            var frontendUrl = _config["FrontendUrl"];
            var confirmationLink = $"{frontendUrl}/confirm-email?email={userCreated.Email}&token={WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token))}";
            await _mailService.SendEmailAsync(req.Email, "Email Confirmation", $"Confirm your email by <a href='{confirmationLink}'>clicking here</a>.");
            return Ok(new { Message = "User registered successfully" });
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordVm req)
        {
            var response = new ApiResponse<string>(); // Tạo đối tượng trước

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userEmail == null)
            {
                response.StatusCode = 400;
                response.Message = "User email not found.";
                return BadRequest(response);
            }

            var user = await _userManager.FindByEmailAsync(userEmail.Value);
            if (user == null)
            {
                response.StatusCode = 400;
                response.Message = "User not found.";
                return BadRequest(response);
            }

            var result = await _userManager.ChangePasswordAsync(user, req.CurrentPassword, req.NewPassword);
            if (!result.Succeeded)
            {
                response.StatusCode = 400;
                response.Message = "Password change failed.";
                response.Data = string.Join("\n", result.Errors.Select(e => e.Description));
                return BadRequest(response);
            }

            // Thành công
            response.StatusCode = 200;
            response.Message = "Password changed successfully.";
            response.Data = null;
            return Ok(response);
        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest req)
        {
            var response = new ApiResponse<string>();
            

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                response.StatusCode = 400;
                response.Message = "User not found.";
                return BadRequest(response);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var frontendUrl = _config["FrontendUrl"];

            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
            var callbackUrl = $"{frontendUrl}/reset-password?email={user.Email}&token={encodedToken}";


            await _mailService.SendEmailAsync(user.Email, "Reset Password",
                               $"Please reset your password by <a href='{callbackUrl}'>clicking here</a>.");

            // Thành công
            response.StatusCode = 200;
            response.Message = "Reset password link sent successfully.";
            response.Data = null;
            return Ok(response);
        }
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordVm req)
        {
            var response = new ApiResponse<string>(); 

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                response.StatusCode = 400;
                response.Message = "User not found.";
                return BadRequest(response);
            }
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(req.Token));
            var result = await _userManager.ResetPasswordAsync(user, decodedToken, req.NewPassword);
            if (!result.Succeeded)
            {
                response.StatusCode = 400;
                response.Message = "Password reset failed.";
                response.Data = string.Join("\n", result.Errors.Select(e => e.Description));
                return BadRequest(response);
            }

            // Thành công
            response.StatusCode = 200;
            response.Message = "Password reset successfully.";
            response.Data = null;
            return Ok(response);
        }
        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailVm req)
        {
            var response = new ApiResponse<string>(); 

            var user = await _userManager.FindByEmailAsync(req.Email);
            if (user == null)
            {
                response.StatusCode = 400;
                response.Message = "User not found.";
                return BadRequest(response);
            }
            var decodedToken = Encoding.UTF8.GetString(Convert.FromBase64String(req.Token));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (!result.Succeeded)
            {
                response.StatusCode = 400;
                response.Message = "Email confirmation failed.";
                response.Data = string.Join("\n", result.Errors.Select(e => e.Description));
                return BadRequest(response);
            }

            // Thành công
            response.StatusCode = 200;
            response.Message = "Email confirmed successfully.";
            response.Data = null;
            return Ok(response);
        }

    }
}
