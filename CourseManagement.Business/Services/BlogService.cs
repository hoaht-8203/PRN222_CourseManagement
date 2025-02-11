using CourseManagement.Business.Services.IService;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Model;
using System.Linq.Expressions;

namespace CourseManagement.Business.Services
{
    public class BlogService : IBlogService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Blog entity)
        {
            await _unitOfWork.Blog.AddAsync(entity);
            await _unitOfWork.SaveAsync(); 
        }

        public async Task DeleteAsync(int id)
        {
            var Blog = await _unitOfWork.Blog.GetFirstOrDefaultAsync(b => b.Id == id);
            if (Blog != null)
            {
                _unitOfWork.Blog.Remove(Blog);
                await _unitOfWork.SaveAsync(); 
            }
        }

        public async Task<IEnumerable<Blog>> GetAllAsync(Expression<Func<Blog, bool>>? filter = null, string? includeProperties = null)
        {
            return await _unitOfWork.Blog.GetAllAsync(filter, includeProperties);
        }
        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _unitOfWork.Blog.GetFirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task UpdateAsync(Blog entity)
        {
            _unitOfWork.Blog.UpdateAsync(entity);
            await _unitOfWork.SaveAsync();
        }
        public async Task<Blog> GetFirstOrDefaultAsync(Expression<Func<Blog, bool>> filter, string? includeProperties = null)
        {

            return await _unitOfWork.Blog.GetFirstOrDefaultAsync(filter, includeProperties);
        }
    }
}
