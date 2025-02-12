using System.ComponentModel.DataAnnotations;

namespace BlazorAppSecure.Model {
    public class CoursesModel {
        public int Index { get; set; }
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
        public string CategoryName { get; set; }
        public List<ModuleModel> ListModule { get; set; }
    }

    public class CourseModel {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Preview image is required")]
        public string PreviewImage { get; set; }
        [Required(ErrorMessage = "Preview video url image is required")]
        public string PreviewVideoUrl { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Level is required")]
        public int Level { get; set; } = 1;
        [Required(ErrorMessage = "Is pro course is required")]
        public bool IsProCourse { get; set; }
    }

    public class CourseSearchModel {
        public string? Title { get; set; }
        public IEnumerable<int>? Levels { get; set; }
        public IEnumerable<int>? Statuss { get; set; }
        public IEnumerable<int>? CategoryIds { get; set; }
        public IEnumerable<int>? CourseTypes { get; set; }
    }

    public enum CourseLevel {
        Beginner = 0,
        Intermediate = 1,
        Advanced = 2,
        Expert = 3
    }

    public enum CourseStatus {
        Available = 1,
        UnAvailable = 0,
        InProgress = 2
    }

    public enum CourseType {
        FreeCourse = 0,
        ProCourse = 1,
    }
}
