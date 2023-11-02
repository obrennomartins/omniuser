using FluentValidation;

namespace OmniUser.Domain.Models.Validations;

public class EnderecoValidation : AbstractValidator<Endereco>
{
    public EnderecoValidation()
    {
        RuleFor(endereco => endereco.Logradouro)
            .Length(2, 200)
            .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(endereco => endereco.Numero)
            .Length(1, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(endereco => endereco.Complemento)
            .MaximumLength(100).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres");

        RuleFor(endereco => endereco.Cep)
            .Matches("^[0-9]{8}$").WithMessage("O campo {PropertyName} precisa ter 8 dígitos");

        RuleFor(endereco => endereco.Bairro)
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(endereco => endereco.Cidade)
            .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(endereco => endereco.Uf)
            .Length(2, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}