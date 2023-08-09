using System.Linq.Expressions;
using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IUsuarioRepository : IDisposable
{
    Task<Usuario?> Adicionar(Usuario usuario);
    Task<Usuario?> Atualizar(Usuario entity);
    Task<Usuario?> Obter(int id);
    Task<Usuario?> ObterUsuarioComEndereco(int id);
    Task<IEnumerable<Usuario?>> ObterTodosAtivos();
    Task<IEnumerable<Usuario?>> ObterTodosInativos();
    Task<IEnumerable<Usuario?>> Buscar(Expression<Func<Usuario, bool>> predicate);
}