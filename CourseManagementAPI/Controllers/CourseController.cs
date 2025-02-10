using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase {
        private readonly CourseManagementDb _db;

        public CourseController(CourseManagementDb db)
        {
            _db = db;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public IActionResult Add([FromBody] AddCourseRequest req) {
            var foundCategory = _db.Categories.Find(req.CategoryId);

            if (foundCategory == null) {
                return BadRequest(new {
                    error = $"Category with id {req.CategoryId} not exsited or removed"
                });
            }

            Course newCourse = new Course() {
                Title = req.Title,
                PreviewImage = req.PreviewImage,
                PreviewVideoUrl = req.PreviewVideoUrl,
                Description = req.Description,
                Level = req.Level,
                Status = req.IsProCourse ? CourseStatus.Pro : CourseStatus.InProgress,
                CategoryId = req.CategoryId,
            };

            _db.Courses.Add(newCourse);
            _db.SaveChanges();

            return Ok(new { Message = "Add new course success" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("search")]
        public IActionResult Search([FromQuery] SearchCourseRequest req) {
            var res = _db.Courses
                .Select(c => new {
                    c.Id,
                    c.Title,
                    c.Description,
                    c.PreviewImage,
                    c.PreviewVideoUrl,
                    c.Level,
                    LevelName = c.Level.ToString(),
                    c.Status,
                    StatusName = c.Status.ToString(),
                    c.Enrollments,
                    c.Modules,
                    c.CategoryId,
                    CategoryName = c.Category.Name,
                })
                .ToList();

            if (!string.IsNullOrEmpty(req.Title)) {
                res = res.Where((c) => c.Title.ToLower().Contains(req.Title.ToLower())).ToList();
            }

            if (req.Levels != null && req.Levels.Any()) {
                res = res.Where((c) => req.Levels.Contains(c.Level)).ToList();
            }

            if (req.Statuss != null && req.Statuss.Any()) {
                res = res.Where((c) => req.Statuss.Contains(c.Status)).ToList();
            }

            if (req.CategoryIds != null && req.CategoryIds.Any()) {
                res = res.Where((c) => req.CategoryIds.Contains(c.CategoryId)).ToList();
            }

            return Ok(res);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public IActionResult Remove([FromBody] RemoveCourseRequest req) {
            var courseFounded = _db.Courses.SingleOrDefault((c) => c.Id.ToString() == req.CourseId && c.Status != CourseStatus.UnAvailable);

            if (courseFounded != null) {
                courseFounded.Status = CourseStatus.UnAvailable;
                _db.Courses.Update(courseFounded);
                _db.SaveChanges();
            }

            return BadRequest(new {
                Error = $"Course {req.CourseId} is not existed or removed"
            });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("detail")]
        public IActionResult Detail([FromQuery] DetailCourseRequest req) {
            var courseFounded = _db.Courses.SingleOrDefault((c) => c.Id.ToString() == req.CourseId && c.Status != CourseStatus.UnAvailable);

            if (courseFounded != null) {
                return Ok(courseFounded);
            }

            return BadRequest(new {
                Error = $"Course {req.CourseId} is not existed or removed"
            });
        }
    }
}
