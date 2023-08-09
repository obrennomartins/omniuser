using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IEnderecoRepository : IDisposable
{
    Task<Endereco?> Adicionar(Endereco endereco);
    Task<Endereco?> Atualizar(Endereco endereco);
    Task<Endereco?> Obter(int id);
    Task<Endereco?> ObterPorUsuarioId(int usuarioId);
    Task<bool> Remover(int id);
}