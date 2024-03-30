namespace OmniUser.API.ViewModels;

public record EnderecoViewModel
{
    public string Logradouro { get; init; } = null!;
    public string Numero { get; init; } = null!;
    public string? Complemento { get; init; }
    public string Cep { get; init; } = null!;
    public string Bairro { get; init; } = null!;
    public string Cidade { get; init; } = null!;
    public string Uf { get; init; } = null!;

    public int UsuarioId { get; init; }
}
