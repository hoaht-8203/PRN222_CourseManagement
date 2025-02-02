using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Model.Model
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public int LessonId { get; set; }
        [ForeignKey("LessonId")]
        public Lesson Lesson { get; set; }
    }
}
