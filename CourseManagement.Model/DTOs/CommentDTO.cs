using CourseManagement.Model.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseManagement.Model.ViewModel;

namespace CourseManagement.Model.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public int LessonId { get; set; }
    }
}
