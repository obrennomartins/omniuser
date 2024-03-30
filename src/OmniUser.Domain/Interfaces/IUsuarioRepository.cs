using System.Linq.Expressions;
using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IUsuarioRepository : IDisposable
{
    /// <summary>
    /// Adiciona um novo <see cref="Usuario" />.
    /// </summary>
    /// <param name="usuario">O usuário a ser adicionado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o usuário adicionado.</returns>
    Task<Usuario?> Adicionar(Usuario usuario);

    /// <summary>
    /// Atualiza um <see cref="Usuario" /> existente.
    /// </summary>
    /// <param name="entity">O usuário a ser atualizado.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o usuário atualizado.</returns>
    Task<Usuario?> Atualizar(Usuario entity);

    /// <summary>
    /// Obtém um <see cref="Usuario" /> pelo ID.
    /// </summary>
    /// <param name="id">O ID do usuário.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. Retorna o usuário encontrado ou nulo se não existir.
    /// </returns>
    Task<Usuario?> Obter(int id);

    /// <summary>
    /// Obtém um usuário com endereço pelo ID.
    /// </summary>
    /// <param name="id">O ID do usuário.</param>
    /// <returns>
    /// Uma tarefa que representa a operação assíncrona. Retorna o usuário com endereço encontrado ou nulo se não
    /// existir.
    /// </returns>
    Task<Usuario?> ObterUsuarioComEndereco(int id);

    /// <summary>
    /// Obtém todos os usuários ativos.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna uma coleção de usuários ativos.</returns>
    Task<IEnumerable<Usuario?>> ObterTodosAtivos();

    /// <summary>
    /// Obtém todos os usuários inativos.
    /// </summary>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna uma coleção de usuários inativos.</returns>
    Task<IEnumerable<Usuario?>> ObterTodosInativos();

    /// <summary>
    /// Busca usuários com base em um predicado.
    /// </summary>
    /// <param name="predicate">O predicado de busca.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna uma coleção de usuários encontrados.</returns>
    Task<IEnumerable<Usuario?>> Buscar(Expression<Func<Usuario, bool>> predicate);
}
