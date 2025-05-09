﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs {
    public class ModuleDTO {
        public class AddModuleRequest {
            [Required]
            public string Title { get; set; }
            [Required]
            public string CourseId { get; set; }
        }

        public class SearchModuleRequest {
            [Required]
            public string CourseId { get; set; }
        }

        public class SearchModuleResponse {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public int Status { get; set; }
            public string CourseId { get; set; }
            public int TotalLesson { get; set; }
        }

        public class DetailModuleRequest {
            [Required]
            public int ModuleId { get; set; }
        }

        public class DetailModuleResponse {
            public int Id { get; set; }
            public string Title { get; set; }
            public int Order { get; set; }
            public int Status { get; set; }
            public string CourseId { get; set; }
        }

        public class RemoveModuleRequest {
            [Required]
            public int ModuleId { get; set; }
        }

        public class UpdateModuleRequest {
            [Required]
            public int ModuleId { get; set; }
            [Required]
            public string Title { get; set; }
            public int? NewOrder { get; set; } = null;
        }

        public class ReorderModuleRequest {
            public List<SearchModuleResponse> NewListModules { get; set; }
            public string CourseId { get; set; }
        }
    }
}
