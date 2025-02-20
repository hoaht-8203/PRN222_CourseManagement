using BlazorAppSecure.Model;

namespace BlazorAppSecure.Sevices.Blog
{
    public interface IBlogService
    {
        Task<List<BlogModel>> GetBlogList();  
        Task<BlogModel> GetBlogById(int id); 
        Task AddBlog(BlogModel blog);         
        Task UpdateBlog(int id, BlogModel blog); 
        Task DeleteBlog(int id);              
    }
}
