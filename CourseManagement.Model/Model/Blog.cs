﻿using CourseManagement.Model.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseManagement.Model.Model
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string? UrlImage { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int ViewCount { get; set; } = 0;
        public BlogStatus Status { get; set; } = BlogStatus.Published;

        public string UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        [JsonIgnore]

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
