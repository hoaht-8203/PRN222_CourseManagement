using Microsoft.AspNetCore.Identity;


namespace CourseManagement.Model.Model
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<LessonProgress> LessonProgresses { get; set; }
    }
}
