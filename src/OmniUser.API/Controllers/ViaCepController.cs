using Microsoft.AspNetCore.Mvc;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
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

    /// <summary>
    /// Obtém os dados de um CEP da API ViaCEP
    /// </summary>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/v1/ViaCep/70150900
    /// <param name="cep">O CEP com 8 dígitos, sem hífen</param>
    /// <returns>Os dados do <see cref="EnderecoViaCep" /></returns>
    /// <response code="400">Se a requisição for mal formada</response>
    /// <response code="500">Se houver um problema interno</response>
    [HttpGet("{cep}")]
    [ProducesResponseType(typeof(EnderecoViaCep), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
