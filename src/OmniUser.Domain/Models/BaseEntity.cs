using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace OmniUser.Domain.Models;

public abstract class BaseEntity
{
    public int Id { get; init; }

    public DateTime CriadoEm { get; init; }
    public DateTime AtualizadoEm { get; init; }

    [NotMapped]
    public ValidationResult ValidationResult { get; protected set; } = new();

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}
