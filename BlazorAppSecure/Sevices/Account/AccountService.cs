using AntDesign;
using BlazorAppSecure.Model;
using CourseManagement.Model.CustomResponses;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorAppSecure.Sevices.Profile
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        public AccountService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Auth");
        }
        
        public async Task<ApiResponse<string>> ChangePassword(ChangePasswordModel changePassword)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Auth/changePassword", changePassword);
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<string>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error changing password.", ex);
            }
        }

        public async Task<UserViewModel> GetMyProfile()
        {
            try
            {

                var response = await _httpClient.GetAsync("api/User/profile");

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var userProfile = JsonSerializer.Deserialize<UserViewModel>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return userProfile ?? throw new Exception("User profile is null");
                }
                else
                {
                    throw new Exception($"Failed to retrieve User profile. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving User profile.", ex);
            }
        }

        public async Task UpdateProfile(string email, UserViewModel userProfile)
        {
            try
            {

                var response = await _httpClient.PutAsJsonAsync("api/User/profile", userProfile);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating profile.", ex);
            }
        }
    }
}
