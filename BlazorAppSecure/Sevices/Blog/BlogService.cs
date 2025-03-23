
using BlazorAppSecure.Model;
using BlazorAppSecure.Sevices.Blog;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.ViewModel;
using System.Net.Http.Json;

namespace BlazorAppSecure.Services.Blog
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;

        public BlogService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Auth");
        }

        public async Task<int> IncrementViewCount(int blogId) {
            try {
                var response = await _httpClient.PostAsync($"api/Blog/increment-view/{blogId}", null);
                if (response.IsSuccessStatusCode) {
                    var result = await response.Content.ReadFromJsonAsync<ViewCountResponse>();
                    return result?.ViewCount ?? 0;
                }
                return 0;
            } catch (Exception) {
                return 0;
            }
        }

        public async Task<List<BlogModel>> GetBlogList(string sortBy = "newest", int? categoryId = null) {
            var queryString = $"api/Blog/list?sortBy={sortBy}";
            if (categoryId.HasValue) {
                queryString += $"&categoryId={categoryId}";
            }

            var response = await _httpClient.GetAsync(queryString);
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<List<BlogModel>>();
            }
            return null;
        }

        public async Task<List<CategoiesModel>> GetCategories() {
            var response = await _httpClient.GetAsync("api/Category/list");
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadFromJsonAsync<List<CategoiesModel>>();
            }
            return new List<CategoiesModel>();
        }

        public async Task<List<BlogModel>> GetBlogList()
        {
            try
            {
               
                var response = await _httpClient.GetAsync("api/Blog/list");

                if (response.IsSuccessStatusCode)
                {
                    var blogs = await response.Content.ReadFromJsonAsync<List<BlogModel>>();
                    return blogs ?? new List<BlogModel>();
                }
                else
                {
                    throw new Exception($"Failed to retrieve Blog list. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Blog list.", ex);
            }
        }


        public async Task<BlogModel> GetBlogById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Blog/detail/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var blog = await response.Content.ReadFromJsonAsync<BlogModel>();
                    return blog ?? throw new Exception($"Blog with ID {id} not found.");
                }
                else
                {
                    throw new Exception($"Failed to get Blog with ID {id}. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Blog with ID {id}.", ex);
            }
        }

        public async Task<bool> AddBlog(BlogVm blog)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Blog/add", blog);

                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to add Blog. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Blog.", ex);
            }
        }


        public async Task UpdateBlog(int id, BlogEditModel blog)
        {
            var blogVm = new BlogVm
            {
                Title = blog.Title,
                Content = blog.Content,
                UrlImage = blog.UrlImage,
                Status = blog.Status,
                CategoryIds = blog.CategoryIds
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync($"api/Blog/update/{id}", blogVm);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to update Blog with ID {id}. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Blog with ID {id}.", ex);
            }
        }

        public async Task UpdateBlogStatus(int blogId, int status)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Blog/update-status/{blogId}", new { status });

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to update Blog status with ID {blogId}. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Blog status with ID {blogId}.", ex);
            }
        }


        public async Task DeleteBlog(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Blog/delete/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to delete Blog with ID {id}. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Blog with ID {id}.", ex);
            }
        }
    }
}