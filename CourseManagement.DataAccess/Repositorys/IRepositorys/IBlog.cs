using CourseManagement.Model.Model;

namespace CourseManagement.DataAccess.Repositorys.IRepositorys
{
    public interface IBlog : IRepository<Blog>
    {
        Task UpdateAsync(Blog booking);
    }
}
