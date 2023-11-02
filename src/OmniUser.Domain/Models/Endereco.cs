using OmniUser.Domain.Models.Validations;

namespace OmniUser.Domain.Models;

public class Endereco : BaseEntity
{
    public Endereco()
    {
    }

    public Endereco(int usuarioId, string logradouro, string numero, string? complemento, string cep, string bairro,
        string cidade, string uf, Usuario usuario)
    {
        UsuarioId = usuarioId;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = string.IsNullOrWhiteSpace(complemento) ? null : complemento;
        Cep = cep;
        Bairro = bairro;
        Cidade = cidade;
        Uf = uf;
        Usuario = usuario;
    }

    public int UsuarioId { get; set; }

    public string Logradouro { get; set; } = null!;
    public string Numero { get; set; } = null!;
    public string? Complemento { get; set; }
    public string Cep { get; set; } = null!;
    public string Bairro { get; set; } = null!;
    public string Cidade { get; set; } = null!;
    public string Uf { get; set; } = null!;

    public Usuario Usuario { get; set; } = null!;

    public override bool EhValido()
    {
        ValidationResult = new EnderecoValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}