using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IUsuarioService : IDisposable
{
    Task<Usuario?> Obter(int idUsuario, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adiciona um novo Usuário.
    /// </summary>
    /// <param name="usuario">O Usuário a ser adicionado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. O Usuário adicionado se a operação for bem-sucedida,
    ///     null caso contrário.
    /// </returns>
    Task<Usuario?> Adicionar(Usuario usuario, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Atualiza um Usuário existente.
    /// </summary>
    /// <param name="usuario">O Usuário com as informações atualizadas.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. O Usuário atualizado se a operação for bem-sucedida,
    ///     null caso contrário.
    /// </returns>
    Task<Usuario?> Atualizar(Usuario usuario, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Ativa um Usuário existente, mudando o seu status.
    /// </summary>
    /// <param name="id">O ID do Usuário a ser ativado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. Retorna true se a operação for bem-sucedida, false caso
    ///     contrário.
    /// </returns>
    Task<bool> Ativar(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Desativa um Usuário existente, mudando o seu status.
    /// </summary>
    /// <param name="id">O ID do Usuário a ser desativado.</param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. Retorna true se a operação for bem-sucedida, false caso
    ///     contrário.
    /// </returns>
    Task<bool> Desativar(int id, CancellationToken cancellationToken = default);

    Task<Endereco?> AdicionarEndereco(Endereco endereco, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Atualiza o Endereço de um usuário.
    /// </summary>
    /// <param name="endereco"></param>
    /// <param name="cancellationToken">O token de cancelamento opcional.</param>
    Task<Endereco?> AtualizarEndereço(Endereco endereco, CancellationToken cancellationToken = default);
}
