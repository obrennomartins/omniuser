using OmniUser.Domain.Models.Validations;

namespace OmniUser.Domain.Models;

public class Usuario : BaseEntity
{
    public Usuario()
    {
    }

    public Usuario(string nome, string? email, string? telefone, string? documento, Endereco? endereco, bool ativo)
    {
        Nome = nome;
        Email = email == string.Empty ? null : email;
        Telefone = telefone == string.Empty ? null : telefone;
        Documento = documento == string.Empty ? null : documento;
        Endereco = endereco;
        Ativo = ativo;
    }

    public string Nome { get; set; } = null!;
    public string? Email { get; set; }
    public string? Telefone { get; set; }
    public string? Documento { get; set; }
    public Endereco? Endereco { get; set; }
    public bool Ativo { get; set; }

    public override bool EhValido()
    {
        ValidationResult = new UsuarioValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}