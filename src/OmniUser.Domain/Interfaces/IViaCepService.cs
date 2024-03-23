using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IViaCepService : IDisposable
{
    Task<EnderecoViaCep?> ObterEndereco(string cep);
}