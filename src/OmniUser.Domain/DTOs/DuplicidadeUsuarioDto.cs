namespace OmniUser.Domain.Dtos;

public record DuplicidadeUsuarioDto
{
    public bool EmailExistente { get; init; }
    public bool DocumentoExistente { get; init; }
    public bool TelefoneExistente { get; init; }
}
