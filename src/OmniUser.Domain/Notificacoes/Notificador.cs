namespace OmniUser.Domain.Notificacoes;

public class Notificador : INotificador
{
    private readonly List<Notificacao> _notificacoes = new();

    public void Handle(Notificacao notificacao)
    {
        _notificacoes.Add(notificacao);
    }

    public IEnumerable<Notificacao> ObterNotificacoes()
    {
        return _notificacoes;
    }

    public bool TemNotificacao()
    {
        return _notificacoes.Any();
    }
}
