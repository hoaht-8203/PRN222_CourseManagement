using CourseManagement.DataAccess.Repositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase {
        private readonly LessonRepository lessonRepository;

        public LessonController(LessonRepository lessonRepository) {
            this.lessonRepository = lessonRepository;
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddLessonRequest req) {
            try {
                await lessonRepository.CreateLesson(req);
                return Ok(new { Message = "Add lesson successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while creating the lesson" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchLessonRequest req) {
            try {
                var lessons = await lessonRepository.SearchLesson(req);
                return Ok(lessons);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while searching lessons" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateLessonRequest req) {
            try {
                await lessonRepository.UpdateLesson(req);
                return Ok(new { Message = "Update lesson successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while updating the lesson" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public async Task<IActionResult> Remove([FromBody] RemoveLessonRequest req) {
            try {
                await lessonRepository.RemoveLesson(req);
                return Ok(new { Message = "Remove lesson successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while removing the lesson" });
            }
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("reorder-lessons")]
        public async Task<IActionResult> ReorderLesson([FromBody] ReorderLessonsRequest req) {
            try {
                await lessonRepository.ReorderLessons(req);
                return Ok(new { Message = "Reorder list lessons successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while reorder the lesson list" });
            }
        }
    }
}
