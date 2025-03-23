using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagement.DataAccess.Repositorys
{
   public class OrderRepository : Repository<Order>, IOrder
    {
        private readonly CourseManagementDb _db;

        public OrderRepository(CourseManagementDb db) : base(db)
        {
        }

        public async Task UpdateAsync(Order order)
        {
        _db.Orders.Update(order);
        }
    }
}
