using BlazorAppSecure.Model;
using BlazorAppSecure.Services.Blog;
using System.Net.Http.Json;
using System.Net;
using BlazorAppSecure.Sevices.Blog;
using System.Net.Http;

namespace BlazorAppSecure.Services.Blog
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient _httpClient;

        public BlogService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Auth");
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


        public async Task AddBlog(BlogModel blog)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Blog/add", blog);

                if (!response.IsSuccessStatusCode)
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

        public async Task UpdateBlog(int id, BlogModel blog)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Blog/update/{id}", blog);

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