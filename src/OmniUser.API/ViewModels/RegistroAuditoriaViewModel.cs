namespace OmniUser.API.ViewModels;

public record RegistroAuditoriaViewModel
{
    public int Id { get; init; }
    public string? Usuario { get; init; }
    public string Entidade { get; init; } = null!;
    public string Acao { get; init; } = null!;
    public string Timestamp { get; init; } = null!;
    public string Alteracoes { get; init; } = null!;
}
