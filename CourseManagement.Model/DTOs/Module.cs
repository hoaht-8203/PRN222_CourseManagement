using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs {
    public class AddModuleRequest {
        [Required]
        public string Title { get; set; }
        [Required]
        public string CourseId { get; set; }
        public int? PrevModuleId { get; set; }
        public int? NextModuleId { get; set; }
    }

    public class SearchModuleRequest {
        [Required]
        public string CourseId { get; set; }
    }

    public class RemoveModuleRequest {
        [Required]
        public int ModuleId { get; set; }
    }

    public class UpdateModuleRequest {
        [Required] 
        public string ModuleId { get; set; }
        [Required]
        public string Title { get; set; }
        public int? PrevModuleId { get; set; }
        public int? NextModuleId { get; set; }
    }
}
