using BlazorAppSecure.Model;
using CourseManagement.Model.CustomResponses;

namespace BlazorAppSecure.Sevices.Profile
{
    public interface IAccountService
    {
        Task<UserViewModel> GetMyProfile();
        Task UpdateProfile(string email, UserViewModel userProfile);
        Task<ApiResponse<string>> ChangePassword(ChangePasswordModel changePassword);
    }
}
