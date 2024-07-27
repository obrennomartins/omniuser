namespace OmniUser.Domain.Models;

public abstract class EntidadeRastreada : EntidadeBase
{
    public DateTime CriadoEm { get; private set; }
    public DateTime AtualizadoEm { get; private set; }

    public void DefinirDataDeCriacao(DateTime data)
    {
        CriadoEm = data;
    }

    public void DefinirDataDeAtualizacao(DateTime data)
    {
        AtualizadoEm = data;
    }
}
