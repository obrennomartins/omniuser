using System.Net.Http.Json;
using System.Text.Json;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.ViaCep;

public class ViaCepRepository : IViaCepRepository
{
    private readonly HttpClient _httpClient;

    public ViaCepRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<EnderecoViaCep?> ObterEndereco(string cep)
    {
        var resposta = await _httpClient.GetAsync($"/ws/{cep}/json");

        if (!resposta.IsSuccessStatusCode) return null;

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var endereco = await resposta.Content.ReadFromJsonAsync<EnderecoViaCep>(options);

        return endereco;
    }
}