using System.Linq.Expressions;
using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IBaseRepository<TEntity> : IDisposable where TEntity : BaseEntity
{
    Task<TEntity?> Adicionar(TEntity entity);
    Task<TEntity?> Atualizar(TEntity entity);
    Task<bool> Remover(int id);
    Task<TEntity?> Obter(int id);
    Task<IEnumerable<TEntity>> ObterTodos();
    Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
}