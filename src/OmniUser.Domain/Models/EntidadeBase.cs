using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace OmniUser.Domain.Models;

public abstract class EntidadeBase
{
    public int Id { get; init; }

    [NotMapped]
    public ValidationResult ValidationResult { get; protected set; } = new();

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}
