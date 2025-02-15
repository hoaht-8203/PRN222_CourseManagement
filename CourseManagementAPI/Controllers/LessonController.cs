using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase {
        private readonly CourseManagementDb _db;

        public LessonController(CourseManagementDb db) {
            _db = db;
        }

        /*
         - Khi set[new item] len dau tien thi:
            1.set prev[new item] = Null, next[new item] = Id[old first item]
            2.set prev[old first item] = Id[new item]
         - Khi set item vao giua thi:
            1.set prev[new item] = id[target prev item], next[new item] = id[target next item]
            2.set next[target prev item] = id[new item], prev[target next item] = id[new item]
         - Khi set item vao cuoi thi
            1.set next[new item] = Null, set prev[new item] = id[old last item]
        */
        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> Add(AddLessonRequest req) {
            var foundedModule = _db.Modules.Find(req.ModuleId);
            if (foundedModule == null) {
                return BadRequest(new { Message = $"Module with id {req.ModuleId} is not existed or removed" });
            }

            if (req.PrevLessonId != null) {
                var foundedPrevLesson = _db.Lessons.Find(req.PrevLessonId);
                if (foundedPrevLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {req.PrevLessonId} is not existed or removed" });
                }
            }

            if (req.NextLessonId != null) {
                var foundedNextLesson = _db.Lessons.Find(req.NextLessonId);
                if (foundedNextLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {req.NextLessonId} is not existed or removed" });
                }
            }

            Lesson newLesson = new Lesson() {
                Title = req.Title,
                Description = req.Description,
                UrlVideo = req.UrlVideo,
                ModuleId = req.ModuleId,
                PrevLessonId = req.PrevLessonId,
                NextLessonId = req.NextLessonId,
            };

            _db.Lessons.Add(newLesson);
            _db.SaveChanges();

            if (newLesson.PrevLessonId == null && newLesson.NextLessonId != null) {
                var oldFirstLesson = _db.Lessons.OrderBy(l => l.Id).First();
                if (oldFirstLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {newLesson.NextLessonId} is not existed or removed" });
                }

                oldFirstLesson.PrevLessonId = newLesson.Id;
                _db.Update(oldFirstLesson);
            }

            if (newLesson.PrevLessonId != null && newLesson.NextLessonId == null) {
                var oldLastLesson = _db.Lessons.OrderBy(l => l.Id).Where(l => l.Id != newLesson.Id).Last();
                if (oldLastLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {newLesson.PrevLessonId} is not existed or removed" });
                }

                oldLastLesson.NextLessonId = newLesson.Id;
                _db.Update(oldLastLesson);
            }

            if (newLesson.PrevLessonId != null && newLesson.NextLessonId != null) {
                var oldPrevLesson = _db.Lessons.Find(newLesson.PrevLessonId);
                var oldNextLesson = _db.Lessons.Find(newLesson.NextLessonId);

                if (oldPrevLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {newLesson.PrevLessonId} is not existed or removed" });
                }

                if (oldNextLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {newLesson.NextLessonId} is not existed or removed" });
                }

                oldNextLesson.PrevLessonId = newLesson.Id;
                _db.Update(oldNextLesson);

                oldPrevLesson.NextLessonId = newLesson.Id;
                _db.Update(oldPrevLesson);
            }

            _db.SaveChanges();

            return Ok(new { Message = "Add lesson successfully" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("search")]
        public async Task<IActionResult> Search(SearchLessonRequest req) {
            var foundedModule = _db.Modules.Find(req.ModuleId);

            if (foundedModule == null) {
                return BadRequest(new { Message = $"Module with id {req.ModuleId} is not existed or removed" });
            }

            var lessons = _db.Lessons
                .Where(l => l.ModuleId == foundedModule.Id)
                .Select(l => new {
                    l.Id,
                    l.Title,
                    l.Description,
                    l.UrlVideo,
                    l.ModuleId,
                    ModuleName = l.Module.Title,
                    l.PrevLessonId,
                    l.NextLessonId
                })
                .ToList();

            var sortedLessons = new List<object>();

            var firstLesson = lessons.FirstOrDefault(l => l.PrevLessonId == null && l.NextLessonId != null);
            if (firstLesson != null) {
                sortedLessons.Add(firstLesson);
                var currentLesson = firstLesson;

                while (currentLesson.NextLessonId != null) {
                    var nextLesson = lessons.FirstOrDefault(l => l.Id == currentLesson.NextLessonId);
                    if (nextLesson == null) break;

                    sortedLessons.Add(nextLesson);
                    currentLesson = nextLesson;
                }
            }

            var endLessons = lessons
                .Where(l => l.NextLessonId == null && l.PrevLessonId != null && !sortedLessons.Contains(l))
                .ToList();
            sortedLessons.AddRange(endLessons);

            var independentModules = lessons
                .Where(l => l.PrevLessonId == null && l.NextLessonId == null)
                .ToList();
            sortedLessons.AddRange(independentModules);

            var remainingModules = lessons
                .Where(m => !sortedLessons.Contains(m))
                .ToList();
            sortedLessons.AddRange(remainingModules);

            return Ok(sortedLessons);
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("update")]
        public async Task<IActionResult> Update(UpdateLessonRequest req) {
            var foundedLesson = _db.Lessons.Find(req.Id);

            if (foundedLesson == null) {
                return BadRequest(new { Message = $"Lesson with id {req.Id} is not existed or removed" });
            }

            if (req.PrevLessonId != null) {
                var foundedPrevLesson = _db.Lessons.Find(req.PrevLessonId);
                if (foundedPrevLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {req.PrevLessonId} is not existed or removed" });
                }
            }

            if (req.NextLessonId != null) {
                var foundedNextLesson = _db.Lessons.Find(req.NextLessonId);
                if (foundedNextLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {req.NextLessonId} is not existed or removed" });
                }
            }

            foundedLesson.Title = req.Title;
            foundedLesson.Description = req.Description;
            foundedLesson.UrlVideo = req.UrlVideo;
            foundedLesson.PrevLessonId = req.PrevLessonId;
            foundedLesson.NextLessonId = req.NextLessonId;

            _db.Update(foundedLesson);

            if (req.PrevLessonId == null && req.NextLessonId != null) {
                var oldFirstLesson = _db.Lessons.Find(foundedLesson.NextLessonId);
                if (oldFirstLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {foundedLesson.NextLessonId} is not existed or removed" });
                }

                oldFirstLesson.PrevLessonId = foundedLesson.Id;
                _db.Update(oldFirstLesson);
            }

            if (foundedLesson.PrevLessonId != null && foundedLesson.NextLessonId == null) {
                var oldLastLesson = _db.Lessons.Find(foundedLesson.PrevLessonId);
                if (oldLastLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {foundedLesson.PrevLessonId} is not existed or removed" });
                }

                oldLastLesson.NextLessonId = foundedLesson.Id;
                _db.Update(oldLastLesson);
            }

            if (foundedLesson.PrevLessonId != null && foundedLesson.NextLessonId != null) {
                var oldPrevLesson = _db.Lessons.Find(foundedLesson.PrevLessonId);
                var oldNextLesson = _db.Lessons.Find(foundedLesson.NextLessonId);

                if (oldPrevLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {foundedLesson.PrevLessonId} is not existed or removed" });
                }

                if (oldNextLesson == null) {
                    return BadRequest(new { Message = $"Lesson with id {foundedLesson.NextLessonId} is not existed or removed" });
                }

                oldNextLesson.PrevLessonId = foundedLesson.Id;
                _db.Update(oldNextLesson);

                oldPrevLesson.NextLessonId = foundedLesson.Id;
                _db.Update(oldPrevLesson);
            }

            _db.SaveChanges();

            return Ok(new { Message = "Update lesson successfully" });
        }

        [Authorize(Roles = Role.Role_User_Admin)]
        [HttpPost("remove")]
        public async Task<IActionResult> Remove(RemoveLessonRequest req) {
            var foundedLesson = _db.Lessons.Find(req.LessonId);

            if (foundedLesson == null) {
                return BadRequest(new { Message = $"Lesson with id {req.LessonId} is not existed or removed" });
            }

            if (foundedLesson.PrevLessonId == null && foundedLesson.NextLessonId != null) {
                var nextLesson = _db.Lessons.Find(foundedLesson.NextLessonId);
                if (nextLesson == null) {
                    return BadRequest(new { Message = $"Internal server error" });
                }

                nextLesson.PrevLessonId = null;
                _db.Update(nextLesson);
            }

            if (foundedLesson.PrevLessonId != null && foundedLesson.NextLessonId == null) {
                var prevLesson = _db.Lessons.Find(foundedLesson.PrevLessonId);
                if (prevLesson == null) {
                    return BadRequest(new { Message = $"Internal server error" });
                }

                prevLesson.NextLessonId = null;
                _db.Update(prevLesson);
            }

            if (foundedLesson.PrevLessonId != null && foundedLesson.NextLessonId != null) {
                var prevLesson = _db.Lessons.Find(foundedLesson.PrevLessonId);
                var nextLesson = _db.Lessons.Find(foundedLesson.NextLessonId);
                if (prevLesson == null || nextLesson == null) {
                    return BadRequest(new { Message = $"Internal server error" });
                }

                prevLesson.NextLessonId = nextLesson.Id;
                nextLesson.PrevLessonId = prevLesson.Id;
                _db.Update(prevLesson);
                _db.Update(nextLesson);
            }

            _db.SaveChanges();

            return Ok(new { Message = "Remove lesson successfully" });
        }
    }
}
