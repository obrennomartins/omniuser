using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace OmniUser.Domain.Models;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }

    [NotMapped] public ValidationResult ValidationResult { get; protected set; } = new();

    public virtual bool EhValido()
    {
        throw new NotImplementedException();
    }
}