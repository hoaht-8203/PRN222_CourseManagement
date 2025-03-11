using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.Model
{
    public class CourseLearningOutcome
    {
        public int Id { get; set; }
        public string Outcome { get; set; }
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
