using Microsoft.AspNetCore.Mvc;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ViaCepController : BaseController
{
    private readonly IViaCepService _viaCepService;

    public ViaCepController(INotificador notificador,
        IViaCepService viaCepService) : base(notificador)
    {
        _viaCepService = viaCepService;
    }

    [HttpGet("{cep}")]
    public async Task<IActionResult> ObterEndereco(string cep)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var endereco = await _viaCepService.ObterEndereco(cep);
        return CustomResponse(endereco);
    }
}