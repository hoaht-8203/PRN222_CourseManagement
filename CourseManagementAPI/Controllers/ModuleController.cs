using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase {
        private readonly CourseManagementDb _db;

        public ModuleController(CourseManagementDb db) {
            _db = db;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public IActionResult Add([FromBody] AddModuleRequest req) {
            if (!Guid.TryParse(req.CourseId, out Guid courseGuid)) {
                return BadRequest(new { Message = "Invalid Course ID format" });
            }

            var findCourse = _db.Courses.Find(courseGuid);

            if (findCourse == null) {
                return BadRequest(new { Message = $"Course with id {req.CourseId} not existed or removed" });
            }

            Module newModule = new Module() {
                Title = req.Title,
                CourseId = findCourse.Id,
            };

            _db.Modules.Add(newModule);
            _db.SaveChanges();

            return Ok(new { Message = "Add new module success" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("seach")]
        public IActionResult Search([FromQuery] SearchModuleRequest req) {
            if (!Guid.TryParse(req.CourseId, out Guid courseGuid)) {
                return BadRequest(new { Message = "Invalid Course ID format" });
            }

            var findCourse = _db.Courses.Find(courseGuid);

            if (findCourse == null) {
                return BadRequest(new { Message = $"Course with id {req.CourseId} not existed or removed" });
            }

            var listModule = _db.Modules
               .Where(m => m.CourseId == findCourse.Id)
               .Select(m => new {
                   m.Id,
                   m.Title,
                   m.CourseId,
                   CourseName = m.Course.Title
               })
               .ToList();

            return Ok(listModule);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public IActionResult Remove([FromBody] RemoveModuleRequest req) {
            var findModule = _db.Modules.Find(req.ModuleId);
            if (findModule == null) {
                return BadRequest(new { Message = $"Module with id {req.ModuleId} is not existed or removed" });
            }

            _db.Remove(findModule);
            _db.SaveChanges();

            return Ok(new { Message = "Remove module successfully" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public IActionResult Update([FromBody] UpdateModuleRequest req) {

            var findModule = _db.Modules.Find(req.ModuleId);
            if (findModule == null) {
                return BadRequest(new { Message = $"Module with id {req.ModuleId} is not existed or removed" });
            }
            
            findModule.Title = req.Title;

            _db.Update(findModule);
            _db.SaveChanges();

            return Ok(new { Message = "Update module successfully" });
        }
    }
}
