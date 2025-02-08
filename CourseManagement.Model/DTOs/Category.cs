using CourseManagement.Model.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs {
    public class CreateCategoryReq {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class DetailCategoryReq {
        [Required]
        public int Id { get; set; }
    }

    public class UpdateCategoryReq {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }

    public class RemoveCategoryReq {
        [Required]
        public int Id { get; set; }
    }
}
