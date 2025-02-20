using CourseManagement.Model.Constant;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CourseManagement.Model.Model
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int? Order {  get; set; }
        public ModuleStatus Status { get; set; } = ModuleStatus.Active;

        public Guid CourseId { get; set; }
        [JsonIgnore]
        public Course Course { get; set; }
        [JsonIgnore]
        public ICollection<Lesson> Lessons { get; set; }
    }
}
