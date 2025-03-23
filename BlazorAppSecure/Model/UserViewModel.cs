using CourseManagement.Model.Constant;

namespace BlazorAppSecure.Model
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public VipStatus VipStatus { get; set; }         
        public DateTime? VipExpirationDate { get; set; }
        public decimal? VipPrice { get; set; }
    }
}
