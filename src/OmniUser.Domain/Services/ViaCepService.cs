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

    public async Task<EnderecoViaCepDto?> ObterEndereco(string cep)
    {
        // ReSharper disable once InvertIf
        if (cep.Length != 8 || !cep.All(char.IsDigit))
        {
            Notificar("O CEP precisa ter exatamente 8 dígitos, sem traço");
            return null;
        }

        return await _viaCepRepository.ObterEndereco(cep);
    }

    ~ViaCepService()
    {
        Dispose();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}