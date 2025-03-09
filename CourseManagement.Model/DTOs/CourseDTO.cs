using CourseManagement.Model.Constant;
using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Model.DTOs {
    public class AddCourseRequest {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PreviewImage { get; set; }
        [Required]
        public string PreviewVideoUrl { get; set; }
        [Required]
        public CourseLevel Level { get; set; }
        [Required]
        public bool IsProCourse { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

    public class RemoveCourseRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class DetailCourseRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class DetailCourseResponse {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreviewImage { get; set; }
        public string PreviewVideoUrl { get; set; }
        public int Level { get; set; }
        public string LevelName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public int CategoryId { get; set; }
        public int CourseType { get; set; }
        public string TypeName { get; set; }
        public string CategoryName { get; set; }

        public List<Module> Modules { get; set; } = [];

        public class Module {
            public int Id { get; set; }
            public string Title { get; set; }
            public string CourseId { get; set; }
            public int Order { get; set; }
            public string CourseName { get; set; }

            public List<Lesson> Lessons { get; set; } = [];
        }
        public class Lesson {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string UrlVideo { get; set; }
            public int ModuleId { get; set; }
            public int Order { get; set; }
            public string ModuleName { get; set; }
            public TimeSpan? VideoDuration { get; set; }
        }
    }

    public class SearchCourseRequest {
        public string? Title { get; set; }
        public List<CourseLevel>? Levels { get; set; }
        public List<CourseStatus>? Statuss { get; set; }
        public List<CourseType>? CourseTypes { get; set; }
        public List<int>? CategoryIds { get; set; }
    }

    public class SearchCourseResponse {
        public int Index { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreviewImage { get; set; }
        public string PreviewVideoUrl { get; set; }
        public CourseLevel Level { get; set; }
        public string LevelName { get; set; }
        public CourseStatus Status { get; set; }
        public string StatusName { get; set; }
        public int CategoryId { get; set; }
        public CourseType CourseType { get; set; }
        public string TypeName { get; set; }
        public string CategoryName { get; set; }
        public int TotalEnrolled { get; set; }
        public int TotalLesson { get; set; }
        public TimeSpan TotalTime { get; set; }
    }

    public class UpdateCourseRequest() {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PreviewImage { get; set; }
        [Required]
        public string PreviewVideoUrl { get; set; }
        [Required]
        public CourseLevel Level { get; set; }
        [Required]
        public bool IsProCourse { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
