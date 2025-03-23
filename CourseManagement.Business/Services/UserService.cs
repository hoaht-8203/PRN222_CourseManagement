using CourseManagement.Business.Services;
using CourseManagement.Business.Services.IService;
using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IRoleService _roleService;

        public UserService(UserManager<AppUser> userManager, IRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
        }


        public async Task<List<AppUserVm>> GetAllUsers()
        {
            var users = await _userManager.Users
                .Include(u => u.Enrollments)
                .Include(u => u.Comments)
                .ToListAsync();

            var response = users.Select(user => new AppUserVm
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = _userManager.GetRolesAsync(user).Result.ToList(),
                Status = user.LockoutEnd == null,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                VipStatus = user.VipStatus,
                VipExpirationDate = user.VipExpirationDate,
                VipPrice = user.VipPrice
            }).ToList();

            return response;
        }


        public async Task<AppUserVm> GetUserByEmail(string emailId)
        {
            var user = await _userManager.Users
                .Include(u => u.Enrollments)
                .Include(u => u.Comments)
                .FirstOrDefaultAsync(u => u.Email == emailId);

            if (user == null) return null;

            return new AppUserVm
            {
                Id = Guid.Parse(user.Id),
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = (await _userManager.GetRolesAsync(user)).ToList(),
                Status = user.LockoutEnd == null,
                EmailConfirmed = user.EmailConfirmed,
                PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                VipStatus = user.VipStatus,
                VipExpirationDate = user.VipExpirationDate,
                VipPrice = user.VipPrice
            };
        }


        public async Task<bool> DeleteUserByEmail(string emailId)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUser(string emailId, AppUserVm user)
        {
            var userIdentity = await _userManager.FindByEmailAsync(emailId);
            if (userIdentity == null) return false;

            userIdentity.UserName = string.IsNullOrWhiteSpace(user.UserName) ? userIdentity.UserName : user.UserName; ;
            userIdentity.FullName = user.FullName;
            userIdentity.Email = user.Email;
            userIdentity.PhoneNumber = user.PhoneNumber;
            userIdentity.VipStatus = user.VipStatus;
            userIdentity.VipExpirationDate = user.VipExpirationDate;
            // userIdentity.LockoutEnd = user.Status ? null : DateTimeOffset.MaxValue;

            var updateResponse = await _userManager.UpdateAsync(userIdentity);
            if (updateResponse.Succeeded)
            {
                var currentUserRoles = await _userManager.GetRolesAsync(userIdentity);
                var rolesToRemove = currentUserRoles.Except(user.Roles);
                var rolesToAdd = user.Roles.Except(currentUserRoles);

                await _userManager.RemoveFromRolesAsync(userIdentity, rolesToRemove);
                var assignRoleResult = await _roleService.AddUserRoleAsync(userIdentity.Email, rolesToAdd.ToArray());
                return assignRoleResult;
            }
            return false;
        }

        public async Task<bool> SetUserBanStatus(string emailId, bool isBanned)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user == null)
                return false;

            user.LockoutEnabled = isBanned;
            user.LockoutEnd = isBanned ? DateTimeOffset.MaxValue : null;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserVipStatus(string emailId, VipStatus vipStatus, decimal vipPrice)
        {
            var user = await _userManager.FindByEmailAsync(emailId);
            if (user == null)
                return false;

            user.VipStatus = vipStatus;
            user.VipPrice = vipPrice;
            user.VipExpirationDate = DateTime.UtcNow.AddMonths(1); 

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }




    }


}
