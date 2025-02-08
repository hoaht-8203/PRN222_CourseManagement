using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase {
        private readonly CourseManagementDb _db;

        public CategoryController(CourseManagementDb db) {
            _db = db;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public IActionResult Add([FromBody] CreateCategoryReq req) {
            Category category = new Category() {
                Name = req.Name,
                Description = req.Description,
            };

            _db.Categories.Add(category);
            _db.SaveChanges();

            return Ok(new { Message = "Add new category success" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("list")]
        public IActionResult List() {
            var res = _db.Categories.ToList();

            return Ok(res);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("detail")]
        public IActionResult Detail([FromQuery] DetailCategoryReq req) {
            var detailCategory = _db.Categories.FirstOrDefault((c) => c.Id == req.Id);

            if (detailCategory != null) {
                return Ok(detailCategory);
            }

            return BadRequest(new {
                error = $"Category with id {req.Id} is not existed or removed"
            });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public IActionResult Update([FromQuery] UpdateCategoryReq req) {
            var findCategory = _db.Categories.FirstOrDefault((c) => c.Id == req.Id);

            if (findCategory != null) {
                findCategory.Name = req.Name;
                findCategory.Description = req.Description;
                _db.Categories.Update(findCategory);
                _db.SaveChanges();

                return Ok(new {
                    message = "Update category successfull"
                });
            }

            return BadRequest(new {
                error = $"Category with id {req.Id} is not existed or removed"
            });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public IActionResult Remove([FromQuery] RemoveCategoryReq req) {
            var findCategory = _db.Categories.Include((c) => c.Courses).FirstOrDefault((c) => c.Id == req.Id);

            if (findCategory != null) {
                if (findCategory.Courses.Count() >= 1) {
                    return BadRequest(new {
                        error = $"This category has {findCategory.Courses.Count()} course item, cannot delete"
                    });
                }

                _db.Categories.Remove(findCategory);
                _db.SaveChanges();

                return Ok(new {
                    message = "Remove category successfull"
                });
            }

            return BadRequest(new {
                error = $"Category with id {req.Id} is not existed or removed"
            });
        }
    }
}
