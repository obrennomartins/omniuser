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

    [HttpGet]
    [ProducesResponseType(typeof(List<RegistroAuditoriaViewModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RegistroAuditoriaViewModel>>> ObterTodos()
    {
        return Ok(_mapper.Map<IEnumerable<RegistroAuditoriaViewModel>>(await _repository.ObterTodos()));
    }
}