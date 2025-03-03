using BlazorAppSecure.Model;
using System.Net.Http.Json;

namespace BlazorAppSecure.Sevices.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _httpClient;

        public CategoryService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient.CreateClient("Auth");
        }

        public async Task<List<CategoiesModel>> GetCategoryList()
        {
            try
            {

                var response = await _httpClient.GetAsync("api/Category/list");

                if (response.IsSuccessStatusCode)
                {
                    var Categorys = await response.Content.ReadFromJsonAsync<List<CategoiesModel>>();
                    return Categorys ?? new List<CategoiesModel>();
                }
                else
                {
                    throw new Exception($"Failed to retrieve Category list. Status Code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving Category list.", ex);
            }
        }

        public async Task<bool> AddCategory(CategoryModel category)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Category/add", category);
                if (response.IsSuccessStatusCode)
                {
                    return true; 
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to add Category. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding Category.", ex);
            }
        }
        public async Task UpdateCategory(int id, CategoiesModel Category)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/Category/update/{id}", Category);

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to update Category with ID {id}. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating Category with ID {id}.", ex);
            }
        }


        public async Task DeleteCategory(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Category/delete/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to delete Category with ID {id}. Status Code: {response.StatusCode}. Error: {errorMessage}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Category with ID {id}.", ex);
            }
        }
    }
}
