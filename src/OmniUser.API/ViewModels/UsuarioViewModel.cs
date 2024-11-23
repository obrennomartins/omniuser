namespace OmniUser.API.ViewModels;

public record UsuarioViewModel
{
    public int? Id { get; init; }
    public string Nome { get; init; } = null!;
    public string? Documento { get; init; }
    public string? Email { get; init; }
    public string? Telefone { get; init; }
    public bool? Ativo { get; init; }

    public EnderecoViewModel? Endereco { get; init; }
}
