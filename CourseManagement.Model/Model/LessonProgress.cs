using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.Model {
    public class LessonProgress {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public bool IsCompleted { get; set; }
    }
}
