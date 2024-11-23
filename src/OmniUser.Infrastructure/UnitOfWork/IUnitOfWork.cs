namespace OmniUser.Infrastructure.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    void BeginTransaction();
    void CommitAsync();
    void Rollback();
}
