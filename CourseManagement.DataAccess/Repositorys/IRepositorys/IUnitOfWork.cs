namespace CourseManagement.DataAccess.Repositorys.IRepositorys
{
    public interface  IUnitOfWork : IDisposable
    {
        IBlog Blog { get; }
        IOrder Order { get; }
        Task SaveAsync();
    }
}
