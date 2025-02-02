

using CourseManagement.Model.ViewModel;

namespace CourseManagement.Business.Services.IService
{
    public interface IRoleService
    {
        Task<List<RoleVm>> GetRolesAsync();

        Task<List<string>> GetUserRolesAsync(string emailId);

        Task<List<string>> AddRolesAsync(string[] roles);

        Task<bool> AddUserRoleAsync(string userEmail, string[] roles);

    }
}
