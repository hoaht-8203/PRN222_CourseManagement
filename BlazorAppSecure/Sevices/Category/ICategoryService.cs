using BlazorAppSecure.Model;

namespace BlazorAppSecure.Sevices.Category
{
    public interface ICategoryService
    {
        Task<List<CategoiesModel>> GetCategoryList();

        Task<bool> AddCategory(CategoryModel Category);
        Task UpdateCategory(int id, CategoiesModel Category);
        Task DeleteCategory(int id);
    }
}
