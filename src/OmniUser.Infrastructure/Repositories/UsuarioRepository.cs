using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Infrastructure.Context;

namespace OmniUser.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IBaseRepository<Usuario> _baseRepository;
    private readonly OmniUserDbContext _db;
    private bool _disposed;

    public UsuarioRepository(OmniUserDbContext db, IBaseRepository<Usuario> baseRepository)
    {
        _baseRepository = baseRepository;
        _db = db;
    }

    public async Task<Usuario?> Adicionar(Usuario usuario)
    {
        return await _baseRepository.Adicionar(usuario);
    }

    public async Task<Usuario?> Atualizar(Usuario entity)
    {
        return await _baseRepository.Atualizar(entity);
    }

    public async Task<Usuario?> Obter(int id)
    {
        return await _baseRepository.Obter(id);
    }

    public async Task<Usuario?> ObterUsuarioComEndereco(int id)
    {
        return await _db.Usuarios.AsNoTracking()
            .Include(usuario => usuario.Endereco)
            .FirstOrDefaultAsync(usuario => usuario.Id == id);
    }

    public async Task<IEnumerable<Usuario?>> ObterTodosAtivos()
    {
        return await _db.Usuarios.AsNoTracking()
            .Where(usuario => usuario.Ativo)
            .Include(usuario => usuario.Endereco)
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario?>> ObterTodosInativos()
    {
        return await _db.Usuarios.AsNoTracking()
            .Where(usuario => !usuario.Ativo)
            .Include(usuario => usuario.Endereco)
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario?>> Buscar(Expression<Func<Usuario, bool>> predicate)
    {
        return await _baseRepository.Buscar(predicate);
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