using OmniUser.Domain.Dtos;
using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IUsuarioRepository : IDisposable
{

    Task<Usuario?> Adicionar(Usuario usuario);

    /// <summary>
    ///     Adiciona um novo <see cref="Usuario" />.
    /// </summary>
    /// <param name="usuario">O usuário a ser adicionado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o usuário adicionado.</returns>
    Task<Usuario?> Adicionar(Usuario usuario, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Atualiza um <see cref="Usuario" /> existente.
    /// </summary>
    /// <param name="usuario">O usuário a ser atualizado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o usuário atualizado.</returns>
    Task<Usuario?> Atualizar(Usuario usuario, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Obtém um <see cref="Usuario" /> pelo ID.
    /// </summary>
    /// <param name="id">O ID do usuário.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. Retorna o usuário encontrado ou nulo se não existir.
    /// </returns>
    Task<Usuario?> Obter(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Obtém todos os usuários ativos.
    /// </summary>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna uma coleção de usuários ativos.</returns>
    Task<IEnumerable<Usuario?>> ObterTodosAtivos(CancellationToken cancellationToken = default);

    /// <summary>
    ///     Obtém todos os usuários inativos.
    /// </summary>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna uma coleção de usuários inativos.</returns>
    Task<IEnumerable<Usuario?>> ObterTodosInativos(CancellationToken cancellationToken = default);

    Task<DuplicidadeUsuarioDto?> VerificarDuplicidade(string? email = null, string? telefone = null, string? documento = null, CancellationToken cancellationToken = default);
}
