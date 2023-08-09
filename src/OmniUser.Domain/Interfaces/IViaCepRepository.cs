using OmniUser.Infrastructure.ViaCep;

namespace OmniUser.Domain.Interfaces;

public interface IViaCepRepository
{
    Task<EnderecoViaCepDto?> ObterEndereco(string cep);
}