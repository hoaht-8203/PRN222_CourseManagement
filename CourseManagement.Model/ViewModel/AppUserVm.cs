
using CourseManagement.Model.Model;

namespace CourseManagement.Model.ViewModel
{
    public class AppUserVm
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Status { get; set; }
        public List<string> Roles { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        public List<Comment>? Comments { get; set; }
    }

  
}
