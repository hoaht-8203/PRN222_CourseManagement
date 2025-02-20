using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CourseManagement.Model.Model {
    public class Category {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Course> Courses { get; set; }
        [JsonIgnore]
        public virtual ICollection<Blog> Blogs { get; set; }
    }
}
