using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using System.Linq.Expressions;

namespace CourseManagement.Business.Services
{
     public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Order entity)
        {
            await _unitOfWork.Order.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var Order = await _unitOfWork.Order.GetFirstOrDefaultAsync(b => b.Id == id);
            if (Order != null)
            {
                _unitOfWork.Order.Remove(Order);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync(Expression<Func<Order, bool>>? filter = null, string? includeProperties = null)
        {
            return await _unitOfWork.Order.GetAllAsync(filter, includeProperties);
        }
        public async Task<Order> GetByIdAsync(int id)
        {
            return await _unitOfWork.Order.GetFirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Order entity)
        {
            _unitOfWork.Order.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }
        public async Task<Order> GetFirstOrDefaultAsync(Expression<Func<Order, bool>> filter, string? includeProperties = null)
        {

            return await _unitOfWork.Order.GetFirstOrDefaultAsync(filter, includeProperties);
        }
    }
}
