using System.Data;
using OmniUser.Domain.Interfaces;
using OmniUser.Infrastructure.Repositories;
using OmniUser.Infrastructure.Session;

namespace OmniUser.Infrastructure.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private bool _disposed;
    private readonly OmniUserDbSession _session;

    public UnitOfWork(OmniUserDbSession session)
    {
        _session = session;
    }

    public void BeginTransaction()
    {
        _session.Connection.BeginTransaction();
    }

    public void CommitAsync()
    {
        _session.Transaction?.Commit();
        Dispose();
    }

    public void Rollback()
    {
        _session.Transaction?.Rollback();
        Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UnitOfWork()
    {
        Dispose();
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _session.Dispose();
        }

        _disposed = true;
    }
}
