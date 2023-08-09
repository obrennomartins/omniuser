using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IUsuarioService : IDisposable
{
    Task<Usuario?> Adicionar(Usuario usuario);
    Task<Usuario?> Atualizar(Usuario usuario);
    Task<bool> Ativar(int id);
    Task<bool> Desativar(int id);

    Task<Endereco?> AtualizarEndereço(Endereco endereco);
}