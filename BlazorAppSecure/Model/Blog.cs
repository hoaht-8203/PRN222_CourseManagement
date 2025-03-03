using BlazorAppSecure.Model;
using CourseManagement.Model.Constant;

public class BlogModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string? UrlImage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ViewCount { get; set; } = 0;
    public string UserId { get; set; }
    public BlogStatus Status { get; set; }
    public List<int> CategoryIds { get; set; }
    public List<CategoiesModel> Categories { get; set; } = new();
}


public class BlogEditModel
{
    public int Id { get; set; } 
    public string Title { get; set; }
    public string Content { get; set; }
    public string? UrlImage { get; set; }
    public BlogStatus Status { get; set; }
    public IEnumerable<int> CategoryIds { get; set; } 
}


public class BlogSearchModel
{
    public string? Title { get; set; }
    public List<int>? CategoryIds { get; set; }
    public List<int>? Statuses { get; set; }
}

