
using CourseManagement.Model.Model;
using System.Text.Json.Serialization;

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
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public List<string> Roles { get; set; }
        public List<Enrollment>? Enrollments { get; set; }
        [JsonIgnore]
        public List<Comment>? Comments { get; set; }
    }

  
}
