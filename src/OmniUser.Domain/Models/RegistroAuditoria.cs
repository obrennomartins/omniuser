namespace OmniUser.Domain.Models;

public class RegistroAuditoria : BaseEntity
{
    public string? Usuario { get; set; }
    public string Entidade { get; set; } = null!;
    public string Acao { get; set; } = null!;
    public DateTime Timestamp { get; set; }
    public string Alteracoes { get; set; } = null!;
}
