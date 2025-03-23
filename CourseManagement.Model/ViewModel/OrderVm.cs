using CourseManagement.Model.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.ViewModel
{
   public class OrderVm
    {
   
            public decimal TotalAmount { get; set; }
            public VipStatus PurchasedPlan { get; set; }
            public decimal VipPrice { get; set; }
            public DateTime? VipExpirationDate { get; set; }

    }
}
