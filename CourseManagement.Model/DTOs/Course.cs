using CourseManagement.Model.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs {
    public class AddCourseRequest {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string PreviewImage { get; set; }
        [Required]
        public string PreviewVideoUrl { get; set; }
        [Required]
        public CourseLevel Level { get; set; }
        [Required]
        public bool IsProCourse { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

    public class RemoveCourseRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class DetailCourseRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class SearchCourseRequest {
        public string? Title { get; set; }
        public List<CourseLevel>? Levels { get; set; }
        public List<CourseStatus>? Statuss { get; set; }
        public List<CourseType>? CourseTypes { get; set; }
        public List<int>? CategoryIds { get; set; }
    }
}
