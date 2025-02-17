using CourseManagement.DataAccess.Data;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                Status = CourseStatus.InProgress,
                CourseType = req.IsProCourse ? CourseType.ProCourse : CourseType.FreeCourse,
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
                    c.CategoryId,
                    c.CourseType,
                    typeName = c.CourseType.ToString(),
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

            if (req.CourseTypes != null && req.CourseTypes.Any()) {
                res = res.Where((c) => req.CourseTypes.Contains(c.CourseType)).ToList();
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
        public async Task<IActionResult> Detail([FromQuery] DetailCourseRequest req) {
            /*var courseFounded = await _db.Courses
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
                    c.CategoryId,
                    c.CourseType,
                    typeName = c.CourseType.ToString(),
                    CategoryName = c.Category.Name,
                    Modules = c.Modules
                        .Select(m => new {
                            m.Id,
                            m.Title,
                            m.PreModuleId,
                            m.NextModuleId,
                            m.CourseId,
                            CourseName = m.Course.Title,
                            Lessons = m.Lessons.Select(l => new {
                                l.Id,
                                l.Title,
                                l.Description,
                                l.UrlVideo,
                                l.ModuleId,
                                ModuleName = l.Module.Title,
                                l.PrevLessonId,
                                l.NextLessonId
                            }).ToList()
                        }).ToList()
                })
                .SingleOrDefaultAsync((c) => c.Id.ToString() == req.CourseId && c.Status != CourseStatus.UnAvailable);

            if (courseFounded != null) {
                var modules = courseFounded.Modules;
                var sortedModules = new List<object>();

                var firstModule = modules.FirstOrDefault(m => m.PreModuleId == null && m.NextModuleId != null);
                if (firstModule != null) {
                    sortedModules.Add(firstModule);
                    var currentModule = firstModule;

                    while (currentModule.NextModuleId != null) {
                        var nextModule = modules.FirstOrDefault(m => m.Id == currentModule.NextModuleId);
                        if (nextModule == null) break;
                        sortedModules.Add(nextModule);
                        currentModule = nextModule;
                    }
                }

                var endModules = modules
                    .Where(m => m.NextModuleId == null && m.PreModuleId != null && !sortedModules.Contains(m))
                    .ToList();
                sortedModules.AddRange(endModules);

                var independentModules = modules
                    .Where(m => m.PreModuleId == null && m.NextModuleId == null)
                    .ToList();
                sortedModules.AddRange(independentModules);

                var remainingModules = modules
                    .Where(m => !sortedModules.Contains(m))
                    .ToList();
                sortedModules.AddRange(remainingModules);

                var sortedModulesWithSortedLessons = sortedModules.Select(moduleObj => {
                    var module = (dynamic)moduleObj;
                    var lessons = ((IEnumerable<dynamic>)module.Lessons).ToList();
                    var sortedLessons = new List<dynamic>();

                    Func<dynamic, bool> firstLessonPredicate = l => l.PrevLessonId == null && l.NextLessonId != null;
                    var firstLesson = lessons.FirstOrDefault(firstLessonPredicate);

                    if (firstLesson != null) {
                        sortedLessons.Add(firstLesson);
                        dynamic currentLesson = firstLesson;

                        while (currentLesson.NextLessonId != null) {
                            var nextLessonId = currentLesson.NextLessonId;
                            Func<dynamic, bool> nextLessonPredicate = l => l.Id == nextLessonId;
                            var nextLesson = lessons.FirstOrDefault(nextLessonPredicate);
                            if (nextLesson == null) break;
                            sortedLessons.Add(nextLesson);
                            currentLesson = nextLesson;
                        }
                    }

                    Func<dynamic, bool> endLessonsPredicate = l =>
                        l.NextLessonId == null &&
                        l.PrevLessonId != null &&
                        !sortedLessons.Contains(l);
                    var endLessons = lessons.Where(endLessonsPredicate).ToList();
                    sortedLessons.AddRange(endLessons);

                    Func<dynamic, bool> independentLessonsPredicate = l =>
                        l.PrevLessonId == null &&
                        l.NextLessonId == null;
                    var independentLessons = lessons.Where(independentLessonsPredicate).ToList();
                    sortedLessons.AddRange(independentLessons);

                    Func<dynamic, bool> remainingLessonsPredicate = l => !sortedLessons.Contains(l);
                    var remainingLessons = lessons.Where(remainingLessonsPredicate).ToList();
                    sortedLessons.AddRange(remainingLessons);

                    return new {
                        module.Id,
                        module.Title,
                        module.PreModuleId,
                        module.NextModuleId,
                        module.CourseId,
                        module.CourseName,
                        Lessons = sortedLessons
                    };
                }).ToList();

                var response = new {
                    courseFounded.Id,
                    courseFounded.Title,
                    courseFounded.Description,
                    courseFounded.PreviewImage,
                    courseFounded.PreviewVideoUrl,
                    courseFounded.Level,
                    courseFounded.LevelName,
                    courseFounded.Status,
                    courseFounded.StatusName,
                    courseFounded.CategoryId,
                    courseFounded.CourseType,
                    courseFounded.typeName,
                    courseFounded.CategoryName,
                    Modules = sortedModulesWithSortedLessons
                };

                return Ok(response);
            }*/

            return BadRequest(new {
                Error = $"Course {req.CourseId} is not existed or removed"
            });
        }
    }
}
