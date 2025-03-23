using CourseManagement.DataAccess.Data;
using CourseManagement.DataAccess.Repositorys.IRepositorys;

namespace CourseManagement.DataAccess.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CourseManagementDb _db;
        public IBlog Blog { get; private set; }
              public IOrder Order { get; private set; }
        public UnitOfWork(CourseManagementDb db)
        {
            _db = db;
            Blog = new BlogRepository(_db);
            Order = new OrderRepository(_db);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
