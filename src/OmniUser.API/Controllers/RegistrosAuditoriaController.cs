using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OmniUser.API.ViewModels;
using OmniUser.Domain.Interfaces;

namespace OmniUser.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RegistrosAuditoriaController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRegistroAuditoriaRepository _repository;

    public RegistrosAuditoriaController(IMapper mapper, IRegistroAuditoriaRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    /// <summary>
    ///     Obtém todos os Registros de Auditoria
    /// </summary>
    /// <remarks>
    ///     Exemplo de requisição:
    /// 
    ///         GET /api/v1/RegistrosAuditoria
    /// </remarks>
    /// <returns>Uma lista de <see cref="RegistroAuditoriaViewModel" /></returns>
    /// <response code="200">Retorna a lista de <see cref="RegistroAuditoriaViewModel" /></response>
    /// <response code="400">Se a requisição for mal formada</response>
    /// <response code="500">Se houver um problema interno</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<RegistroAuditoriaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<RegistroAuditoriaViewModel>>> ObterTodos()
    {
        return Ok(_mapper.Map<IEnumerable<RegistroAuditoriaViewModel>>(await _repository.ObterTodos()));
    }
}
