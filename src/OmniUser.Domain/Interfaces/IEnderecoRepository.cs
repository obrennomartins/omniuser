using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IEnderecoRepository : IDisposable
{
    /// <summary>
    ///     Adiciona um novo <see cref="Endereco" />.
    /// </summary>
    /// <param name="endereco">O endereço a ser adicionado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o usuário adicionado.</returns>
    Task<Endereco?> Adicionar(Endereco endereco, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Atualiza um <see cref="Endereco" /> existente.
    /// </summary>
    /// <param name="endereco">O endereço a ser atualizado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o endereço atualizado.</returns>
    Task<Endereco?> Atualizar(Endereco endereco, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Obtém um <see cref="Endereco" /> pelo ID.
    /// </summary>
    /// <param name="id">O ID do endereço.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o endereço encontrado ou nulo se não existir.</returns>
    Task<Endereco?> Obter(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Obtém um <see cref="Endereco" /> pelo ID do usuário.
    /// </summary>
    /// <param name="idUsuario">o ID do usuário associado ao endereço.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna o endereço encontrado para o usuário ou nulo se não existir.</returns>
    Task<Endereco?> ObterPorUsuarioId(int usuarioId, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Remove um <see cref="Endereco" /> pelo ID.
    /// </summary>
    /// <param name="id">o ID do endereço a ser removido</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna </returns>
    Task<bool> Remover(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Remove um <see cref="Endereco" /> pelo ID do usuário.
    /// </summary>
    /// <param name="id">o ID do usuário cujo endereço deve ser removido.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>Uma tarefa que representa a operação assíncrona. Retorna </returns>
    Task<bool> RemoverPorUsuarioId(int usuarioId, CancellationToken cancellationToken = default);
}
