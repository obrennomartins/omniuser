using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IViaCepService : IDisposable
{
    Task<EnderecoViaCepDto?> ObterEndereco(string cep);
}