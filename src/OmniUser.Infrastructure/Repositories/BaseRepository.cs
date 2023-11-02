using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Infrastructure.Context;

namespace OmniUser.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
{
    private readonly OmniUserDbContext _db;
    private readonly DbSet<TEntity> _dbSet;
    private bool _disposed;

    public BaseRepository(OmniUserDbContext db)
    {
        _db = db;
        _dbSet = db.Set<TEntity>();
    }

    public async Task<TEntity?> Adicionar(TEntity entity)
    {
        _dbSet.Add(entity);

        if (await SalvarAlteracoesAsync())
        {
            return entity;
        }

        return null;
    }

    public async Task<TEntity?> Atualizar(TEntity entity)
    {
        _dbSet.Update(entity);

        if (await SalvarAlteracoesAsync())
        {
            return entity;
        }

        return null;
    }

    public async Task<bool> Remover(int id)
    {
        _dbSet.Remove(new TEntity {Id = id});
        return await SalvarAlteracoesAsync();
    }

    public async Task<TEntity?> Obter(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> ObterTodos()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private async Task<bool> SalvarAlteracoesAsync()
    {
        return await _db.SaveChangesAsync() > 0;
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
        }

        _disposed = true;
    }
}