using CourseManagement.Model.Constant;

namespace CourseManagement.Model.ViewModel
{
    public class BlogVm
    {
        
        public string Title { get; set; }
        public string Content { get; set; }
        public string? UrlImage { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public int ViewCount { get; set; } = 0;
        public BlogStatus Status { get; set; } = BlogStatus.Published;
        public IEnumerable<int> CategoryIds { get; set; }
    }
}
