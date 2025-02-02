using CourseManagement.Business.Services.IService;
using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


    }
}

