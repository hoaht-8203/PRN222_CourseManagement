using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs {
    public class AddLessonRequest {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UrlVideo { get; set; }
        [Required]
        public int ModuleId { get; set; }
    }

    public class SearchLessonRequest {
        public int ModuleId { get; set; }
    }

    public class UpdateLessonRequest {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string UrlVideo { get; set; }
        [Required]
        public int NewOrder {  get; set; }
    }

    public class RemoveLessonRequest {
        [Required]
        public int LessonId { get; set; }
    }
}
