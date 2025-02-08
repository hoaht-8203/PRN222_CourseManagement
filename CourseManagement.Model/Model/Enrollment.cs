using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Model.Model
{
    public class Enrollment
    {
        public string UserId { get; set; } 
        public AppUser User { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
