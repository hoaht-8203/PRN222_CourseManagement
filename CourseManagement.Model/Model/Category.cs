using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseManagement.Model.Model
{
    public class Category {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public ICollection<Course> Courses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
