using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using CourseManagement.Model.ViewModel.CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly CourseManagementDb _db;

        public BlogController(IBlogService blogService, CourseManagementDb db)
        {
            _blogService = blogService;
            _db = db;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _blogService.GetAllAsync(includeProperties: "User,Categories");
            var blogVms = blogs.Select(b => new BlogVm
            {
                Title = b.Title,
                Content = b.Content,
                UrlImage = b.UrlImage,
                CreatedAt = b.CreatedAt,
                ViewCount = b.ViewCount,
                UserId = b.User.Id,
                CategoryIds = b.Categories.Select(c => c.Id).ToList()
            }).ToList();

            return Ok(blogVms);
        }

        [HttpGet("detail")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogService.GetFirstOrDefaultAsync(b => b.Id == id, "User,Categories");
            if (blog == null)
                return NotFound(new { error = $"Blog with id {id} not found" });

            var blogVm = new BlogVm
            {
                Title = blog.Title,
                Content = blog.Content,
                UrlImage = blog.UrlImage,
                CreatedAt = blog.CreatedAt,
                ViewCount = blog.ViewCount,
                UserId = blog.User.Id,
                CategoryIds = blog.Categories.Select(c => c.Id).ToList()
            };

            return Ok(blogVm);
        }

      
        [HttpPost("add")]
        public async Task<IActionResult> Create(BlogVm blogVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var blog = new Blog
            {
                Title = blogVm.Title,
                Content = blogVm.Content,
                UrlImage = blogVm.UrlImage,
                CreatedAt = blogVm.CreatedAt ?? DateTime.UtcNow,
                ViewCount = blogVm.ViewCount,
                UserId = blogVm.UserId
            };

            blog.Categories = await _db.Categories
                                      .Where(c => blogVm.CategoryIds.Contains(c.Id))
                                      .ToListAsync();

            await _blogService.AddAsync(blog);
            return Ok(new { message = "Created blog successfully" });
        }

    
        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, BlogVm blogVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingBlog = await _blogService.GetFirstOrDefaultAsync(b => b.Id == id, "Categories");
            if (existingBlog == null)
            {
                return NotFound(new { error = $"Blog with id {id} not found" });
            }

            existingBlog.Title = blogVm.Title;
            existingBlog.Content = blogVm.Content;
            existingBlog.UrlImage = blogVm.UrlImage;
            existingBlog.ViewCount = blogVm.ViewCount;

          
            existingBlog.Categories = await _db.Categories
                                               .Where(c => blogVm.CategoryIds.Contains(c.Id))
                                               .ToListAsync();

            await _blogService.UpdateAsync(existingBlog);
            return Ok(new { message = "Updated blog successfully" });
        }

     
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingBlog = await _blogService.GetByIdAsync(id);
            if (existingBlog == null)
            {
                return NotFound(new { error = $"Blog with id {id} not found" });
            }

            await _blogService.DeleteAsync(id);
            return Ok(new { message = "Deleted blog successfully" });
        }
    }
}
