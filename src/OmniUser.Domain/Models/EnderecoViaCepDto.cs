namespace OmniUser.Domain.Models;

public record EnderecoViaCepDto
{
    public string Cep { get; init; } = null!;
    public string Logradouro { get; init; } = null!;
    public string Complemento { get; init; } = null!;
    public string Bairro { get; init; } = null!;
    public string Localidade { get; init; } = null!;
    public string Uf { get; init; } = null!;
    public string Ibge { get; init; } = null!;
    public string Gia { get; init; } = null!;
    public string Ddd { get; init; } = null!;
    public string Siafi { get; init; } = null!;
}