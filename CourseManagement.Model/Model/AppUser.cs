using CourseManagement.Model.Constant;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace CourseManagement.Model.Model
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public ICollection<Enrollment>? Enrollments { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<LessonProgress> LessonProgresses { get; set; }
        public virtual ICollection<CourseProgress> CourseProgresses { get; set; }

        public VipStatus VipStatus { get; set; } = VipStatus.Free;
        public DateTime? VipExpirationDate { get; set; }
        public decimal? VipPrice { get; set; } 
    }
}
