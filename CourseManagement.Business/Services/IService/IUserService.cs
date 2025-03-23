using CourseManagement.Model.Constant;
using CourseManagement.Model.ViewModel;

namespace CourseManagement.Business.Services
{
    public interface IUserService
    {
        Task<List<AppUserVm>> GetAllUsers();

        Task<AppUserVm> GetUserByEmail(string emailId);

        Task<bool> UpdateUser(string emailId, AppUserVm user);

        Task<bool> DeleteUserByEmail(string emailId);
        Task<bool> SetUserBanStatus(string emailId, bool isBanned);
        Task<bool> UpdateUserVipStatus(string emailId, VipStatus vipStatus, decimal vipPrice);
    }
}
