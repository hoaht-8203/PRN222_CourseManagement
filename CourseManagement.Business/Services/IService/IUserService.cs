

using CourseManagement.Model.ViewModel;

namespace CourseManagement.Business.Services.IService
{
    public interface IUserService
    {
        Task<List<AppUserVm>> GetAllUsers();

        Task<AppUserVm> GetUserByEmail(string emailId);

        Task<bool> UpdateUser(string emailId, AppUserVm user);

        Task<bool> DeleteUserByEmail(string emailId);
    }
}
