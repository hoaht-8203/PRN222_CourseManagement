using System.ComponentModel.DataAnnotations;

namespace BlazorAppSecure.Model {
    public class CategoiesModel {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CategoyModel {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
