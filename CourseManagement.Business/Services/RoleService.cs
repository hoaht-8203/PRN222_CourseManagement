using CourseManagement.Business.Services.IService;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace CourseManagementAPI.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager,
            UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<List<RoleVm>> GetRolesAsync()
        {
            var roleList = _roleManager.Roles.Select(x =>
            new RoleVm { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
            return roleList;
        }

        public async Task<List<string>> GetUserRolesAsync(string emailId)
        {
            var user = await _userManager.FindByEmailAsync(emailId);

            var userRoles = await _userManager.GetRolesAsync(user);
            return userRoles.ToList();
        }
        public async Task<List<string>> AddRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                    rolesList.Add(role);
                }
            }
            return rolesList;
        }

        public async Task<ApiRes> AddRoleAsync(string role)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(role));

            if (!result.Succeeded)
            {
                return new ApiRes { Success = false, Errors = result.Errors.Select(p => p.Description).ToList() };
            }

            return new ApiRes { Success = true, Message = "Create new role success" };
        }

        public async Task<bool> AddUserRoleAsync(string userEmail, string[] roles)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);

            var exitsRoles = await ExistsRolesAsync(roles);

            if (user != null && exitsRoles.Count == roles.Length)
            {
                var assginRoles = await _userManager.AddToRolesAsync(user, exitsRoles);
                return assginRoles.Succeeded;
            }

            return false;

        }

        private async Task<List<string>> ExistsRolesAsync(string[] roles)
        {
            var rolesList = new List<string>();
            foreach (var role in roles)
            {
                var roleExist = await _roleManager.RoleExistsAsync(role);
                if (roleExist)
                {
                    rolesList.Add(role);
                }
            }
            return rolesList;

        }
        public async Task<ApiRes> UpdateRoleAsync(string oldRoleName, string newRoleName)
        {
            var role = await _roleManager.FindByNameAsync(oldRoleName);
            if (role == null)
                return new ApiRes { Success = false, Errors = ["Role not found."] };

            role.Name = newRoleName;
            var result = await _roleManager.UpdateAsync(role);

            if (!result.Succeeded)
                return new ApiRes { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() };

            return new ApiRes { Success = true, Message = "Role updated successfully." };
        }

        public async Task<ApiRes> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return new ApiRes { Success = false, Errors = ["Role not found."] };

            var result = await _roleManager.DeleteAsync(role);

            if (!result.Succeeded)
                return new ApiRes { Success = false, Errors = result.Errors.Select(e => e.Description).ToList() };

            return new ApiRes { Success = true, Message = "Role deleted successfully." };
        }

    }
}