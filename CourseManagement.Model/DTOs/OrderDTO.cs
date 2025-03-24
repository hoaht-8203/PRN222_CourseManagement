using CourseManagement.Model.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.DTOs
{
    public class SearchOrderRequest {
        public string? UserEmail { get; set; }
        public OrderStatus? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class SearchOrderResponse {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public OrderStatus Status { get; set; }
        public string StatusName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public VipStatus PurchasedPlan { get; set; }
        public string PlanName { get; set; }
        public DateTime? VipExpirationDate { get; set; }
    }

    public class UpdateOrderStatusRequest {
        public int OrderId { get; set; }
        public OrderStatus NewStatus { get; set; }
    }
}
