namespace OmniUser.API.ViewModels;

public record EnderecoViewModel
{
    public string Logradouro { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string? Complemento { get; set; }
    public string Cep { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string UF { get; set; } = null!;

    public int UsuarioId { get; set; }
}