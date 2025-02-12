using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        public virtual ICollection<BlogCategory> BlogCategories { get; set; }
    }
}
