using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.API.Controllers;

public abstract class BaseController : Controller
{
    private readonly INotificador _notificador;

    protected BaseController(INotificador notificador)
    {
        _notificador = notificador;
    }
    
    public bool OperacaoValida()
    {
        return !_notificador.TemNotificacao();
    }

    public ActionResult CustomResponse(object? result = null)
    {
        if (OperacaoValida())
        {
            return Ok(result);
        }

        return BadRequest(new
        {
            success = false,
            errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
        });
    }

    public ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!modelState.IsValid)
        {
            NotificarErroModelInvalida(modelState);
        }

        return CustomResponse();
    }

    public void NotificarErroModelInvalida(ModelStateDictionary modelState)
    {
        var erros = modelState.Values.SelectMany(e => e.Errors);
        foreach (var erro in erros)
        {
            var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotificarErro(errorMsg);
        }
    }

    public void NotificarErro(string mensagem)
    {
        _notificador.Handle(new Notificacao(mensagem));
    }
}