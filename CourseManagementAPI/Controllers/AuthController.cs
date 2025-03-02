using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CourseManagement.Model.ViewModel;
using System.Security.Claims;
using CourseManagement.Model.CustomResponses;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;

        public AuthController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
                response.Data = string.Join("\n", result.Errors.Select(e => e.Description)); // Gộp lỗi thành một chuỗi
                return BadRequest(response);
            }

            // Thành công
            response.StatusCode = 200;
            response.Message = "Password changed successfully.";
            response.Data = null;
            return Ok(response);
        }
    }
}
