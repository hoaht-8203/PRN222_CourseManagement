using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
