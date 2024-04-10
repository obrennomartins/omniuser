using FluentValidation;

namespace OmniUser.Domain.Models.Validations;

public class UsuarioValidation : AbstractValidator<Usuario>
{
    public UsuarioValidation()
    {
        RuleFor(usuario => usuario.Nome)
            .Length(2, 100)
            .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        RuleFor(usuario => usuario.Email)
            .EmailAddress().WithMessage("O campo {PropertyName} precisa ser um e-mail válido")
            .MaximumLength(100).WithMessage("O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres");

        RuleFor(usuario => usuario.Telefone)
            .Matches(@"^\d+$").WithMessage("O campo {PropertyName} precisa ter apenas números")
            .Length(10, 11)
            .WithMessage("O campo {PropertyName} precisa ter os 2 dígitos do DDD e mais 8 ou 9 dígitos do telefone");

        RuleFor(usuario => usuario.Documento)
            .Matches(@"^\d+$").WithMessage("O campo {PropertyName} precisa ter apenas números")
            .Length(1, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");
    }
}
