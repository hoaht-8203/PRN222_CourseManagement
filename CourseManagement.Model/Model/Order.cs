using CourseManagement.Model.Constant;
using CourseManagement.Model.Model;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public VipStatus PurchasedPlan { get; set; } = VipStatus.Premium;
    public decimal VipPrice { get; set; } 

    public DateTime? VipExpirationDate { get; set; }
}
