using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace CourseManagement.Model.Model
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public string UrlVideo { get; set; }

        public int ModuleId { get; set; }
        public Module Module { get; set; }

        public ICollection<Document> Documents { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}