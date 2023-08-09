using System.Net.Http.Json;
using System.Text.Json;
using OmniUser.Domain.Interfaces;

namespace OmniUser.Infrastructure.ViaCep;

public class ViaCepRepository : IViaCepRepository
{
    private readonly HttpClient _httpClient;

    public ViaCepRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://viacep.com.br/");
    }

    public async Task<EnderecoViaCepDto?> ObterEndereco(string cep)
    {
        var resposta = await _httpClient.GetAsync($"/ws/{cep}/json");
        resposta.EnsureSuccessStatusCode();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var endereco = await resposta.Content.ReadFromJsonAsync<EnderecoViaCepDto>(options);

        return endereco;
    }
}