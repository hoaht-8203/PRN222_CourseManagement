using System.ComponentModel.DataAnnotations;

namespace BlazorAppSecure.Model
{


	public class BlogModel
	{
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? UrlImage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ViewCount { get; set; } = 0;
        public string UserId { get; set; }
        public List<int> CategoryIds { get; set; } 

    }

  
}
