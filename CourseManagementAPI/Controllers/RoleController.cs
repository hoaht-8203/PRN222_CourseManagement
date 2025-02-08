using CourseManagement.Business.Services.IService;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {

            var list = await _roleService.GetRolesAsync();
            return Ok(list);
        }

        [Authorize]
        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetuserRole(string userEmail)
        {
            var userClaims = await _roleService.GetUserRolesAsync(userEmail);
            return Ok(userClaims);

        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("addRoles")]
        public async Task<ActionResult> AddRoles(string[] roles)
        {
            var userrole = await _roleService.AddRolesAsync(roles);
            if (userrole.Count == 0)
            {
                return BadRequest();
            }
            return Ok(userrole);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("addRole")]
        public async Task<ActionResult> AddRole([FromBody] RoleRequest request) {
            var result = await _roleService.AddRoleAsync(request.RoleName);
            
            if (!result.Success) {
                return BadRequest(new {
                    Errors = result.Errors
                });
            }

            return Ok(new {
                Message = result.Message
            });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("addUserRoles")]
        public async Task<ActionResult> AddUserRole([FromBody] AddUserVm addUser)
        {
            var result = await _roleService.AddUserRoleAsync(addUser.UserEmail, addUser.Roles);

            if (!result)
            {
                return BadRequest();
            }

            return StatusCode((int)HttpStatusCode.Created, result);
        }

    }
}
