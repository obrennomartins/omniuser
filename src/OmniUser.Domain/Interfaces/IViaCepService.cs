using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IViaCepService : IDisposable
{
    /// <summary>
    /// Obtém os dados de um endereço na API do ViaCEP
    /// </summary>
    /// <param name="cep">O CEP com 8 dígitos, sem hífen</param>
    /// <returns>Os dados do <see cref="EnderecoViaCep" /></returns>
    Task<EnderecoViaCep?> ObterEndereco(string cep);
}
