using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Model.Model
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public ICollection<Lesson> Lessons { get; set; }
    }
}
