using CourseManagement.Model.Constant;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace CourseManagement.Model.Model
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreviewImage { get; set; }
        public string PreviewVideoUrl { get; set; }
        public CourseLevel Level { get; set; } = CourseLevel.Beginner;
        public CourseStatus Status { get; set; } = CourseStatus.InProgress;
        public CourseType CourseType { get; set; } = CourseType.FreeCourse;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Module> Modules { get; set; }
        public List<CourseLearningOutcome> LearningOutcomes { get; set; } = new();
    }
}
