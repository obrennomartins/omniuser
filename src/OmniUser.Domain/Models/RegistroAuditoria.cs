namespace OmniUser.Domain.Models;

public class RegistroAuditoria : EntidadeRastreada
{
    public string? Usuario { get; init; }
    public string Entidade { get; init; } = null!;
    public string Acao { get; init; } = null!;
    public DateTime Timestamp { get; init; }
    public string Alteracoes { get; init; } = null!;
}
