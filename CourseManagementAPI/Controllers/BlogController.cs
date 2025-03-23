using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;
using CourseManagement.Model.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CourseManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly CourseManagementDb _db;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(IBlogService blogService, CourseManagementDb db, UserManager<AppUser> userManager)
        {
            _blogService = blogService;
            _db = db;
            _userManager = userManager;
        }


        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var blogs = await _blogService.GetAllAsync(includeProperties: "User,Categories");

            var blogList = blogs.Select(blog => new
            {
                blog.Id,
                blog.Title,
                blog.Content,
                blog.UrlImage,
                blog.CreatedAt,
                blog.ViewCount,
                blog.UserId,
                blog.Status, 
                Categories = blog.Categories.Select(c => new { c.Id, c.Name, c.Description })
            }).ToList();

            return Ok(blogList);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var blog = await _blogService.GetFirstOrDefaultAsync(b => b.Id == id, "User,Categories");

            if (blog == null)
                return NotFound(new { error = $"Blog with id {id} not found" });

            var blogVm = new
            {
                blog.Id,
                blog.Title,
                blog.Content,
                blog.UrlImage,
                blog.CreatedAt,
                blog.ViewCount,
                blog.UserId,
                blog.Status, 
                Categories = blog.Categories.Select(c => new { c.Id, c.Name, c.Description })
            };

            return Ok(blogVm);
        }


        [HttpPost("add")]
        public async Task<IActionResult> Create(BlogVm blogVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            string emailFromClaims = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(emailFromClaims);

            var blog = new Blog
            {
                Title = blogVm.Title,
                Content = blogVm.Content,
                UrlImage = blogVm.UrlImage,
                CreatedAt = blogVm.CreatedAt ?? DateTime.UtcNow,
                UserId = user.Id
            };

            blog.Categories = await _db.Categories
                                      .Where(c => blogVm.CategoryIds.Contains(c.Id))
                                      .ToListAsync();

            await _blogService.AddAsync(blog);
            return Ok(new { message = "Created blog successfully" });
        }

        [HttpPost("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BlogVm blogVm)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

              
                var existingBlog = await _blogService.GetFirstOrDefaultAsync(b => b.Id == id, "Categories");
                if (existingBlog == null)
                {
                    return NotFound(new { error = $"Blog with id {id} not found" });
                }

                existingBlog.Title = blogVm.Title;
                existingBlog.Content = blogVm.Content;
                existingBlog.UrlImage = blogVm.UrlImage;
                existingBlog.Status = blogVm.Status;
                existingBlog.ViewCount = 0; 

             
                if (blogVm.CategoryIds != null && blogVm.CategoryIds.Any())
                {
                  
                    var newCategories = await _db.Categories
                                                .Where(c => blogVm.CategoryIds.Contains(c.Id))
                                                .ToListAsync();

                    if (newCategories.Count != blogVm.CategoryIds.Count())
                    {
                        return BadRequest(new { error = "One or more category IDs do not exist" });
                    }

                 
                    existingBlog.Categories = newCategories;
                }
                else
                {
                    existingBlog.Categories.Clear();
                }

         
                _db.Blogs.Update(existingBlog);
                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                   
                    return StatusCode(500, new
                    {
                        error = "An error occurred while saving the entity changes",
                        details = ex.InnerException?.Message ?? ex.Message
                    });
                }

                return Ok(new { message = "Updated blog successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An internal server error occurred while updating the blog",
                    details = ex.Message
                });
            }
        }

        [HttpPost("update-status/{blogId}")]
        public async Task<IActionResult> UpdateBlogStatusByBlogId(int blogId, int status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingBlog = await _blogService.GetByIdAsync(blogId);
            if (existingBlog == null)
            {
                return NotFound($"No blog found for BlogId: {blogId}");
            }

            BlogStatus statusType;
            switch (status)
            {
                case 0:
                    statusType = BlogStatus.Draft;
                    break;
                case 1:
                    statusType = BlogStatus.Published;
                    break;          
                default:
                    return BadRequest("Invalid status value. Please provide a valid status.");
            }

            await _blogService.UpdateAsync(existingBlog);
            return Ok(new { message = "Updated blog successfully" });
        }


        [HttpDelete("delete/{id}")]
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
