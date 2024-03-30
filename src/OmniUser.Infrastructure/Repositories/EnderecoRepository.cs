using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly IBaseRepository<Endereco> _baseRepository;
    private bool _disposed;

    public EnderecoRepository(IBaseRepository<Endereco> baseRepository)
    {
        _baseRepository = baseRepository;
    }

    public async Task<Endereco?> Adicionar(Endereco endereco)
    {
        return await _baseRepository.Adicionar(endereco);
    }

    public async Task<Endereco?> Atualizar(Endereco endereco)
    {
        return await _baseRepository.Atualizar(endereco);
    }

    public async Task<Endereco?> Obter(int id)
    {
        return await _baseRepository.Obter(id);
    }

    public async Task<Endereco?> ObterPorUsuarioId(int usuarioId)
    {
        return (await _baseRepository.Buscar(endereco => endereco.UsuarioId == usuarioId)).FirstOrDefault();
    }

    public async Task<bool> Remover(int id)
    {
        return await _baseRepository.Remover(id);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _baseRepository.Dispose();
        }

        _disposed = true;
    }
}
