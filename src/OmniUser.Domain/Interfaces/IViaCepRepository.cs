using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IViaCepRepository
{
    Task<EnderecoViaCep?> ObterEndereco(string cep);
}