using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs
{
    public class AddDocumentRequest {
        [Required]
        public int LessonId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public string File { get; set; }
        public long FileSize { get; set; }
    }

    public class DocumentResponse {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string File { get; set; }
        public DateTime UploadedAt { get; set; }
        public long FileSize { get; set; }
        public int LessonId { get; set; }
    }

    public class RemoveDocumentRequest {
        [Required]
        public int LessonId { get; set; }
        [Required]
        public string File { get; set; }
    }
}
