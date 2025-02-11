using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagement.Model.Model
{
    public class BlogCategory
    {
        public int BlogId { get; set; }
        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
    }
}
