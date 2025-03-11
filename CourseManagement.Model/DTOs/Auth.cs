using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs
{
    public class RegisterSdi
    {
        public string? FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class RoleRequest {
        public string RoleName { get; set; }
    }
}
