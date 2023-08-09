using Microsoft.AspNetCore.Mvc;
using OmniUser.Domain.Interfaces;

namespace OmniUser.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ViaCepController : ControllerBase
{
    private readonly IViaCepRepository _viaCepRepository;

    public ViaCepController(IViaCepRepository viaCepRepository)
    {
        _viaCepRepository = viaCepRepository;
    }

    [HttpGet("{cep}")]
    public async Task<IActionResult> ObterEndereco(string cep)
    {
        var endereco = await _viaCepRepository.ObterEndereco(cep);
        return Ok(endereco);
    }
}