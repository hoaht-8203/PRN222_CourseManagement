using CourseManagement.Business.Services;
using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CourseManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;

        public UserController(SignInManager<AppUser> signInManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            if (userEmail is null)
            {
                return Unauthorized(new { message = "Invalid token" });
            }
            var user = await _userService.GetUserByEmail(userEmail.Value);
            return Ok(user);

        }
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUser([FromBody] AppUserVm userModel)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var result = await _userService.UpdateUser(userEmail.Value, userModel);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userList = await _userService.GetAllUsers();
            return Ok(userList);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("{emailId}")]
        public async Task<IActionResult> Get(string emailId)
        {
            var userList = await _userService.GetUserByEmail(emailId);
            return Ok(userList);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPut("{emailId}")]
        public async Task<IActionResult> UpdateUser(string emailId, [FromBody] AppUserVm userModel)
        {
            var result = await _userService.UpdateUser(emailId, userModel);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpDelete("{emailId}")]
        public async Task<IActionResult> Delete(string emailId)
        {
            var result = await _userService.DeleteUserByEmail(emailId);
            if (!result)
            {
                return BadRequest();
            }
            return NoContent();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] object empty)
        {
            //{}
            if (empty is not null)
            {
                await _signInManager.SignOutAsync();
            }
            return Ok();
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPut("{emailId}/ban")]
        public async Task<IActionResult> BanUser(string emailId, [FromQuery] bool isBanned)
        {
            var result = await _userService.SetUserBanStatus(emailId, isBanned);
            if (!result)
            {
                return BadRequest();
            }
            return Ok(new { Message = isBanned ? "User banned successfully." : "User unbanned successfully." });
        }

        [HttpPut("update-vip-status")]
        public async Task<IActionResult> UpdateVipStatus([FromBody] VipStatusUpdateModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request data");
            }

            var result = await _userService.UpdateUserVipStatus(model.Email, model.VipStatus, model.VipPrice);
            if (!result)
            {
                return BadRequest("Failed to update VIP status");
            }

            return Ok(new { message = "VIP status updated successfully" });
        }
    }

    public class VipStatusUpdateModel
    {
        public string Email { get; set; }
        public VipStatus VipStatus { get; set; }
        public decimal VipPrice { get; set; }
    }
}

