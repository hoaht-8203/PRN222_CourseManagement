using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.Model.Constant
{
     public enum  OrderStatus
    {
        Pending,    // Đang chờ xử lý
        Completed,  // Hoàn thành
        Cancelled   // Đã hủy
    }
}
