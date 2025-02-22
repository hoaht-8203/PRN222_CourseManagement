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

    public class SearchLessonResponse {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UrlVideo { get; set; }
        public int Order { get; set; }
        public int Status { get; set; }
        public int ModuleId { get; set; }
        public ModuleDetail Module { get; set; }

        public class ModuleDetail {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public int Status { get; set; }
            public string CourseId { get; set; }
        }
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

    public class ReorderLessonsRequest {
        [Required]
        public int ModuleId { get; set; }
        [Required]
        public List<SearchLessonResponse> NewListLessons { get; set; }
    }
}
