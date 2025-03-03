using BlazorAppSecure.Model;
using CourseManagement.Model.ViewModel;

namespace BlazorAppSecure.Sevices.Blog
{
    public interface IBlogService
    {
        Task<List<BlogModel>> GetBlogList();
        Task<BlogModel> GetBlogById(int id);
        Task<bool> AddBlog(BlogVm blog); 
        Task UpdateBlog(int id, BlogEditModel blog);
        Task DeleteBlog(int id);
    }
}
