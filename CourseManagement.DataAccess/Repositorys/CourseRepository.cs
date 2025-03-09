using AutoMapper;
using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Constant;
using CourseManagement.Model.DTOs;
using CourseManagement.Model.Model;
using Microsoft.EntityFrameworkCore;

namespace CourseManagement.DataAccess.Repositorys {
    public class CourseRepository : ICourseRepository {
        private readonly CourseManagementDb _context;
        private readonly IMapper _mapper;

        public CourseRepository(CourseManagementDb context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateCourse(AddCourseRequest request) {
            var foundCategory = _context.Categories
                .Find(request.CategoryId) 
                ?? throw new ArgumentException($"Category with id {request.CategoryId} not exsited or removed");

            Course newCourse = new Course() {
                Title = request.Title,
                PreviewImage = request.PreviewImage,
                PreviewVideoUrl = request.PreviewVideoUrl,
                Description = request.Description,
                Level = request.Level,
                Status = CourseStatus.InProgress,
                CourseType = request.IsProCourse ? CourseType.ProCourse : CourseType.FreeCourse,
                CategoryId = foundCategory.Id,
                LearningOutcomes = request.LearningOutcomes.Select(o => new CourseLearningOutcome {
                    Outcome = o
                }).ToList()
            };

            _context.Courses.Add(newCourse);
            await _context.SaveChangesAsync();
        }

        public async Task<DetailCourseResponse> Detail(DetailCourseRequest request) {
            var course = await _context.Courses
                    .Include(c => c.Category)
                    .Include(c => c.LearningOutcomes)
                    .Include(c => c.Modules.Where(m => m.Status == ModuleStatus.Active))
                        .ThenInclude(m => m.Lessons.Where(l => l.Status == LessonStatus.Active))
                    .Where(c => c.Id.ToString() == request.CourseId && c.Status != CourseStatus.UnAvailable)
                    .SingleOrDefaultAsync();

            if (course == null) {
                throw new ArgumentException($"Course {request.CourseId} is not existed or removed");
            }

            course.Modules = course.Modules.OrderBy(m => m.Order).ToList();
            foreach (var module in course.Modules) {
                module.Lessons = module.Lessons.OrderBy(l => l.Order).ToList();
            }

            var response = _mapper.Map<DetailCourseResponse>(course);
            response.LearningOutcomes = course.LearningOutcomes.Select(lo => lo.Outcome).ToList();

            return response;
        }

        public async Task RemoveCourse(RemoveCourseRequest request) {
            var courseFounded = await _context.Courses
                .SingleOrDefaultAsync(c => c.Id.ToString() == request.CourseId && c.Status != CourseStatus.UnAvailable);

            if (courseFounded == null) {
                throw new ArgumentException($"Course {request.CourseId} is not existed or removed");
            }

            courseFounded.Status = CourseStatus.UnAvailable;
            _context.Courses.Update(courseFounded);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SearchCourseResponse>> Search(SearchCourseRequest request) {
            // First query: Get basic course information
            var query = _context.Courses
                .Include(c => c.Modules.Where(m => m.Status == ModuleStatus.Active))
                    .ThenInclude(m => m.Lessons.Where(l => l.Status == LessonStatus.Active))
                .Include(c => c.Enrollments)
                .Include(c => c.Category)
                .Where(c => c.Status != CourseStatus.UnAvailable);  // Only include available courses

            // Apply filters
            if (!string.IsNullOrEmpty(request.Title)) {
                query = query.Where(c => c.Title.ToLower().Contains(request.Title.ToLower()));
            }

            if (request.Levels != null && request.Levels.Count != 0) {
                query = query.Where(c => request.Levels.Contains(c.Level));
            }

            if (request.Statuss != null && request.Statuss.Count != 0) {
                query = query.Where(c => request.Statuss.Contains(c.Status));
            }

            if (request.CourseTypes != null && request.CourseTypes.Count != 0) {
                query = query.Where(c => request.CourseTypes.Contains(c.CourseType));
            }

            if (request.CategoryIds != null && request.CategoryIds.Count != 0) {
                query = query.Where(c => request.CategoryIds.Contains(c.CategoryId));
            }

            // Execute the query and load the data
            var courses = await query.ToListAsync();

            // Second part: Process the data in memory
            var results = courses.Select((c, index) => new SearchCourseResponse {
                Index = index,
                Id = c.Id.ToString(),
                Title = c.Title,
                Description = c.Description,
                PreviewImage = c.PreviewImage,
                PreviewVideoUrl = c.PreviewVideoUrl,
                Level = c.Level,
                LevelName = c.Level.ToString(),
                Status = c.Status,
                StatusName = c.Status.ToString(),
                CategoryId = c.CategoryId,
                CourseType = c.CourseType,
                TypeName = c.CourseType.ToString(),
                CategoryName = c.Category.Name,
                TotalEnrolled = c.Enrollments.Count,
                TotalLesson = c.Modules
                    .Where(m => m.Status == ModuleStatus.Active)
                    .SelectMany(m => m.Lessons.Where(l => l.Status == LessonStatus.Active))
                    .Count(),
                TotalTime = CalculateTotalTime(c.Modules
                    .Where(m => m.Status == ModuleStatus.Active)
                    .SelectMany(m => m.Lessons.Where(l => l.Status == LessonStatus.Active))
                    .Where(l => l.VideoDuration.HasValue)
                    .Select(l => l.VideoDuration.Value))
            }).ToList();

            return results;
        }

        // Helper method to calculate total time
        private TimeSpan CalculateTotalTime(IEnumerable<TimeSpan> durations) {
            if (!durations.Any()) return TimeSpan.Zero;

            long totalTicks = durations.Sum(d => d.Ticks);
            return new TimeSpan(totalTicks);
        }

        public async Task UpdateCourse(UpdateCourseRequest request) {
            var courseFounded = await _context.Courses
               .SingleOrDefaultAsync(c => c.Id.ToString() == request.Id && c.Status != CourseStatus.UnAvailable)
               ?? throw new ArgumentException($"Course with id {request.Id} not exsited or removed");

            var foundCategory = _context.Categories
               .Find(request.CategoryId)
               ?? throw new ArgumentException($"Category with id {request.CategoryId} not exsited or removed");

            courseFounded.Title = request.Title;
            courseFounded.Description = request.Description;
            courseFounded.PreviewImage = request.PreviewImage;
            courseFounded.PreviewVideoUrl = request.PreviewVideoUrl;
            courseFounded.Level = request.Level;
            courseFounded.CourseType = request.IsProCourse ? CourseType.ProCourse : CourseType.FreeCourse;
            courseFounded.CategoryId = request.CategoryId;

            _context.CourseLearningOutcomes.RemoveRange(courseFounded.LearningOutcomes);
            courseFounded.LearningOutcomes = request.LearningOutcomes.Select(o => new CourseLearningOutcome {
                Outcome = o,
                CourseId = courseFounded.Id
            }).ToList();

            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatus(UpdateStatusRequest request) {
            var courseFounded = await _context.Courses
               .SingleOrDefaultAsync(c => c.Id.ToString() == request.CourseId && c.Status != CourseStatus.UnAvailable)
               ?? throw new ArgumentException($"Course with id {request.CourseId} not exsited or removed");

            courseFounded.Status = request.newStatus;

            await _context.SaveChangesAsync();
        }
    }
}
