using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Infrastructure.Context;

namespace OmniUser.Infrastructure.Repositories;

public class RegistroAuditoriaRepository : IRegistroAuditoriaRepository
{
    private readonly IBaseRepository<RegistroAuditoria> _baseRepository;
    private readonly OmniUserDbContext _db;
    private bool _disposed;

    public RegistroAuditoriaRepository(OmniUserDbContext db, IBaseRepository<RegistroAuditoria> baseRepository)
    {
        _baseRepository = baseRepository;
        _db = db;
    }

    public async Task<RegistroAuditoria?> Obter(int id)
    {
        return await _baseRepository.Obter(id);
    }

    public async Task<IEnumerable<RegistroAuditoria?>> ObterTodos()
    {
        return await _baseRepository.ObterTodos();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _db.Dispose();
            _baseRepository.Dispose();
        }

        _disposed = true;
    }
}
