using System.ComponentModel.DataAnnotations;

namespace CourseManagement.Model.Model
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string File { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public long FileSize { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
    }
}
