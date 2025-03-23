

namespace CourseManagement.DataAccess.Repositorys.IRepositorys
{
   public interface IOrder : IRepository<Order>
    {
        Task UpdateAsync(Order order);
    }
}
