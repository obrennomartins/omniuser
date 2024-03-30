using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IUsuarioService : IDisposable
{
    /// <summary>
    ///     Adiciona um novo Usuário.
    /// </summary>
    /// <param name="usuario">O Usuário a ser adicionado.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. O Usuário adicionado se a operação for bem-sucedida,
    ///     null caso contrário.
    /// </returns>
    Task<Usuario?> Adicionar(Usuario usuario);

    /// <summary>
    ///     Atualiza um Usuário existente.
    /// </summary>
    /// <param name="usuario">O Usuário com as informações atualizadas.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. O Usuário atualizado se a operação for bem-sucedida,
    ///     null caso contrário.
    /// </returns>
    Task<Usuario?> Atualizar(Usuario usuario);

    /// <summary>
    ///     Ativa um Usuário existente, mudando o seu status.
    /// </summary>
    /// <param name="id">O ID do Usuário a ser ativado.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. Retorna true se a operação for bem-sucedida, false caso
    ///     contrário.
    /// </returns>
    Task<bool> Ativar(int id);

    /// <summary>
    ///     Desativa um Usuário existente, mudando o seu status.
    /// </summary>
    /// <param name="id">O ID do Usuário a ser desativado.</param>
    /// <returns>
    ///     Uma tarefa que representa a operação assíncrona. Retorna true se a operação for bem-sucedida, false caso
    ///     contrário.
    /// </returns>
    Task<bool> Desativar(int id);

    Task<Endereco?> AtualizarEndereço(Endereco endereco);
}
