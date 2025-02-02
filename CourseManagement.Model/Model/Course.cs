using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CourseManagement.Model.Model
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreviewImage { get; set; }
        public string PreviewVideoUrl { get; set; }
        public string Level { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
