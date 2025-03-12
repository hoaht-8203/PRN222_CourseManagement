using CourseManagement.DataAccess.Repositorys;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static CourseManagement.Model.DTOs.ModuleDTO;

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

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpGet("detail")]
        public async Task<IActionResult> Detail([FromQuery] DetailLessonRequest req) {
            try {
                var res = await lessonRepository.Detail(req);
                return Ok(res);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while get detail the lesson" });
            }
        }

        [Authorize]
        [HttpGet("get-lessons-completed")]
        public async Task<IActionResult> LessonCompleted([FromQuery] GetLessonsCompletedRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                var res = await lessonRepository.GetLessonsCompleted(req, userEmail.Value);
                return Ok(res);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred while get list lesson completed: {ex}" });
            }
        }

        [Authorize]
        [HttpGet("lesson-completed")]
        public async Task<IActionResult> LessonCompleted([FromQuery] CompletedLessonRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                await lessonRepository.CompletedLesson(req, userEmail.Value);
                return Ok(new { Message = "Lesson completed successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred while completed this lesson: {ex}" });
            }
        }

        [Authorize]
        [HttpGet("lesson-not-completed")]
        public async Task<IActionResult> LessonNotCompleted([FromQuery] NotCompletedLessonRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                await lessonRepository.NotCompletedLesson(req, userEmail.Value);
                return Ok(new { Message = "Lesson not completed successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception) {
                return StatusCode(500, new { Error = "An error occurred while not completed this lesson" });
            }
        }

        [Authorize]
        [HttpGet("get-last-viewed")]
        public async Task<IActionResult> GetLastViewed([FromQuery] GetLastViewedRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                var res = await lessonRepository.GetLastViewed(req, userEmail.Value);
                return Ok(res);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred: {ex}" });
            }
        }

        [Authorize]
        [HttpPost("update-last-viewed")]
        public async Task<IActionResult> UpdateLastViewed([FromBody] UpdateLastViewedRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                await lessonRepository.UpdateLastViewed(req, userEmail.Value);
                return Ok(new { Message = "Last viewed lesson updated successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred: {ex}" });
            }
        }

        [Authorize]
        [HttpPost("add-note")]
        public async Task<IActionResult> AddNote([FromBody] AddNoteRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                await lessonRepository.AddNote(req, userEmail.Value);
                return Ok(new { Message = "Note added successfully" });
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred: {ex}" });
            }
        }

        [Authorize]
        [HttpGet("get-notes")]
        public async Task<IActionResult> GetNotes([FromQuery] GetNotesRequest req) {
            try {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
                if (userEmail is null) {
                    return Unauthorized(new { message = "Invalid token" });
                }
                var notes = await lessonRepository.GetNotes(req, userEmail.Value);
                return Ok(notes);
            } catch (ArgumentException ex) {
                return BadRequest(new { Error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Error = $"An error occurred: {ex}" });
            }
        }
    }
}
