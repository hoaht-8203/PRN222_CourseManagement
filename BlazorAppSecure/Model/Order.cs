using CourseManagement.Model.Constant;
using System;

namespace BlazorAppSecure.Model
{
    public class OrderCreateVM
    {
        public decimal TotalAmount { get; set; }
        public VipStatus PurchasedPlan { get; set; }
        public decimal VipPrice { get; set; }
        public DateTime? VipExpirationDate { get; set; }
    }
}