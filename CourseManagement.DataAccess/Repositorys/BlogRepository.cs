using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;
using CourseManagement.Model.Model;

namespace CourseManagement.DataAccess.Repositorys
{
    public class BlogRepository : Repository<Blog>, IBlog
    {
        private readonly CourseManagementDb _db;
        public BlogRepository(CourseManagementDb db) : base(db)
        {
        }

        public async Task UpdateAsync(Blog blog)
        {
            _db.Blogs.Update(blog);
        }
    }
}
