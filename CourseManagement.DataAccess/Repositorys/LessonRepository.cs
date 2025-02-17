using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DataAccess.Repositorys {
    public class LessonRepository : ILessonRepository {
        private readonly CourseManagementDb _context;

        public LessonRepository(CourseManagementDb context) {
            _context = context;
        }

        public async Task CreateLesson(AddLessonRequest req) {
            var module = await _context.Modules
                .Include(m => m.Course)
                .FirstOrDefaultAsync(m => m.Id == req.ModuleId && m.Status == ModuleStatus.Active)
                ?? throw new ArgumentException($"Module with id {req.ModuleId} not found or not active");

            if (module.Course.Status == CourseStatus.UnAvailable) {
                throw new ArgumentException("Cannot add lesson to a module in an unavailable course");
            }

            var maxLessonOrder = await _context.Lessons
                .Where(l => l.ModuleId == req.ModuleId && l.Status == LessonStatus.Active)
                .MaxAsync(l => (int?)l.Order) ?? 0;

            var newLesson = new Lesson {
                Title = req.Title,
                Description = req.Description,
                UrlVideo = req.UrlVideo,
                ModuleId = req.ModuleId,
                Order = maxLessonOrder + 1,
                Status = LessonStatus.Active
            };

            _context.Lessons.Add(newLesson);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLesson(UpdateLessonRequest req) {
            var lesson = await _context.Lessons
                .Include(l => l.Module)
                    .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(l => l.Id == req.Id && l.Status == LessonStatus.Active)
                ?? throw new ArgumentException($"Lesson with id {req.Id} not found or not active");

            if (lesson.Module.Course.Status == CourseStatus.UnAvailable) {
                throw new ArgumentException("Cannot update lesson in an unavailable course");
            }

            if (lesson.Module.Status != ModuleStatus.Active) {
                throw new ArgumentException("Cannot update lesson in an inactive module");
            }

            // Update basic information
            lesson.Title = req.Title;
            lesson.Description = req.Description;
            lesson.UrlVideo = req.UrlVideo;

            // Handle order change if needed
            if (req.NewOrder != lesson.Order) {
                var lessonsList = await _context.Lessons
                    .Where(l => l.ModuleId == lesson.ModuleId && l.Status == LessonStatus.Active)
                    .OrderBy(l => l.Order)
                    .ToListAsync();

                var currentOrder = lesson.Order ?? lessonsList.Count;
                var newOrder = Math.Max(1, Math.Min(req.NewOrder, lessonsList.Count));

                if (newOrder < currentOrder) {
                    foreach (var les in lessonsList) {
                        if (les.Id == lesson.Id) {
                            les.Order = newOrder;
                        } else if (les.Order >= newOrder && les.Order < currentOrder) {
                            les.Order++;
                        }
                    }
                } else if (newOrder > currentOrder) {
                    foreach (var les in lessonsList) {
                        if (les.Id == lesson.Id) {
                            les.Order = newOrder;
                        } else if (les.Order <= newOrder && les.Order > currentOrder) {
                            les.Order--;
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveLesson(RemoveLessonRequest req) {
            var lesson = await _context.Lessons
                .Include(l => l.Module)
                    .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync(l => l.Id == req.LessonId && l.Status == LessonStatus.Active)
                ?? throw new ArgumentException($"Lesson with id {req.LessonId} not found or already removed");

            if (lesson.Module.Course.Status == CourseStatus.UnAvailable) {
                throw new ArgumentException("Cannot remove lesson from an unavailable course");
            }

            if (lesson.Module.Status != ModuleStatus.Active) {
                throw new ArgumentException("Cannot remove lesson from an inactive module");
            }

            var currentOrder = lesson.Order;

            // Mark lesson as deleted
            lesson.Status = LessonStatus.Delete;
            lesson.Order = null;

            // Reorder remaining lessons
            if (currentOrder.HasValue) {
                var lessonsToReorder = await _context.Lessons
                    .Where(l => l.ModuleId == lesson.ModuleId
                            && l.Status == LessonStatus.Active
                            && l.Order > currentOrder)
                    .ToListAsync();

                foreach (var les in lessonsToReorder) {
                    les.Order--;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<Lesson>> SearchModule(SearchLessonRequest req) {
            var module = await _context.Modules
                .FirstOrDefaultAsync(m => m.Id == req.ModuleId && m.Status == ModuleStatus.Active)
                ?? throw new ArgumentException($"Module with id {req.ModuleId} not found or not active");

            var lessons = await _context.Lessons
                .Include(l => l.Documents)
                .Include(l => l.Comments)
                .Where(l => l.ModuleId == req.ModuleId && l.Status == LessonStatus.Active)
                .OrderBy(l => l.Order)
                .ToListAsync();

            return lessons;
        }
    }
}