using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.Domain.Services;

public sealed class ViaCepService : BaseService, IViaCepService
{
    private readonly IViaCepRepository _viaCepRepository;

    public ViaCepService(INotificador notificador,
        IViaCepRepository viaCepRepository) : base(notificador)
    {
        _viaCepRepository = viaCepRepository;
    }

    public async Task<EnderecoViaCep?> ObterEndereco(string cep)
    {
        if (cep.Length != 8 || !cep.All(char.IsDigit))
        {
            Notificar("O CEP precisa ter exatamente 8 dígitos, sem traço");
            return null;
        }

        var resposta = await _viaCepRepository.ObterEndereco(cep);

        // ReSharper disable once InvertIf
        if (resposta?.Cep is null)
        {
            Notificar("Houve um problema ao obter as informações do CEP. Verifique se ele é válido.");
            return null;
        }

        return resposta;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    ~ViaCepService()
    {
        Dispose();
    }
}
