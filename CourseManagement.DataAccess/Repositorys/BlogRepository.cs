using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Model;

namespace CourseManagement.DataAccess.Repositorys
{
    public class BlogRepository : Repository<Blog>, IBlog
    {
        public BlogRepository(CourseManagementDb db) : base(db)
        {
        }

        public Task UpdateAsync(Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}
