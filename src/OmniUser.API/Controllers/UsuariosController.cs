using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OmniUser.API.ViewModels;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsuariosController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _repository;
    private readonly IUsuarioService _service;

    public UsuariosController(IMapper mapper,
        INotificador notificador,
        IUsuarioRepository repository,
        IUsuarioService service) : base(notificador)
    {
        _mapper = mapper;
        _repository = repository;
        _service = service;
    }

    /// <summary>
    /// Obtém todos os Usuários ativos
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    ///
    ///     GET /api/v1/Usuarios
    /// </remarks>
    /// <returns>Uma lista de Usuários</returns>
    /// <response code="200">Retorna a lista de Usuários</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<UsuarioViewModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UsuarioViewModel>>> ObterTodosAtivos()
    {
        return CustomResponse(_mapper.Map<IEnumerable<UsuarioViewModel>>(await _repository.ObterTodosAtivos()));
    }

    /// <summary>
    /// Obtém todos os Usuários inativos
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/v1/Usuarios/Inativos
    /// </remarks>
    /// <returns>Uma lista de Usuários</returns>
    /// <response code="200">Retorna a lista de Usuários inativos</response>
    [HttpGet("Inativos")]
    [ProducesResponseType(typeof(List<UsuarioViewModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<UsuarioViewModel>>> ObterTodosInativos()
    {
        return CustomResponse(_mapper.Map<IEnumerable<UsuarioViewModel>>(await _repository.ObterTodosInativos()));
    }

    /// <summary>
    /// Obtém um Usuário pelo seu Id
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     GET /api/v1/Usuarios/1
    /// </remarks>
    /// <param name="id">Id do Usuário</param>
    /// <returns>Usuário</returns>
    /// <response code="200">Retorna o Usuário com o Id informado</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<UsuarioViewModel>> ObterPorId(int id)
    {
        var usuario = _mapper.Map<UsuarioViewModel>(await _repository.ObterUsuarioComEndereco(id));
        return usuario == null ? NotFound() : CustomResponse(usuario);
    }

    /// <summary>
    /// Adiciona um Usuário
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/v1/Usuarios
    /// 
    ///     {
    ///         "nome": "Fulano da Silva Sauro",
    ///         "email": "fulano.sauro@gmail.com",
    ///         "telefone": "11999999999",
    ///         "documento": "12345678901",
    ///         "endereco": {
    ///             "logradouro": "Rua dos Bobos",
    ///             "numero": "0",
    ///             "complemento": "Apto 123",
    ///             "cep": "12345678",
    ///             "bairro": "Vila do Chaves",
    ///             "cidade": "Cidade de Deus",
    ///             "uf": "AC"
    ///         },
    ///         "ativo": true
    ///     }
    /// </remarks>
    /// <param name="usuarioViewModel"></param>
    /// <returns>Usuário</returns>
    [HttpPost]
    public async Task<ActionResult<UsuarioViewModel>> Adicionar(UsuarioViewModel usuarioViewModel)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var usuario = await _service.Adicionar(_mapper.Map<Usuario>(usuarioViewModel));
        return CustomResponse(_mapper.Map<UsuarioViewModel>(usuario));
    }

    /// <summary>
    /// Atualiza um Usuário
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     PUT /api/v1/Usuarios/1
    /// 
    ///     {
    ///         "nome": "Fulano da Silva Sauro",
    ///         "email": "fulano.sauro@gmail.com",
    ///         "telefone": "11999999999",
    ///         "documento": "12345678901",
    ///         "endereco": {
    ///             "logradouro": "Rua dos Bobos",
    ///             "numero": "0",
    ///             "complemento": "Apto 123",
    ///             "cep": "12345678",
    ///             "bairro": "Vila do Chaves",
    ///             "cidade": "Cidade de Deus",
    ///             "estado": "AC"
    ///         },
    ///         "ativo": true
    ///     }
    /// </remarks>
    /// <param name="id">Id do Usuário</param>
    /// <param name="usuarioViewModel">Usuário</param>
    /// <returns>Usuário</returns>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UsuarioViewModel>> Atualizar(int id, UsuarioViewModel usuarioViewModel)
    {
        if (id != usuarioViewModel.Id)
        {
            NotificarErro("O Id informado não é o mesmo passado na query");
            return CustomResponse(usuarioViewModel);
        }

        if (!ModelState.IsValid)
        {
            return CustomResponse(ModelState);
        }

        await _service.Atualizar(_mapper.Map<Usuario>(usuarioViewModel));

        return CustomResponse(usuarioViewModel);
    }

    /// <summary>
    /// Muda o status de um Usuário para ativo
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/v1/Usuarios/1/Ativar
    /// </remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/Ativar")]
    public async Task<ActionResult<UsuarioViewModel>> Ativar(int id)
    {
        var sucesso = await _service.Ativar(id);

        if (!sucesso)
        {
            return CustomResponse(new
            {
                success = false,
                errors = new List<string> {"Não foi possível ativar o usuário"}
            });
        }

        return CustomResponse();
    }

    /// <summary>
    /// Muda o status de um Usuário para inativo
    /// </summary>
    /// <remarks>
    /// Exemplo de requisição:
    /// 
    ///     POST /api/v1/Usuarios/1/Desativar
    /// </remarks>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("{id:int}/Desativar")]
    public async Task<ActionResult<UsuarioViewModel>> Desativar(int id)
    {
        var sucesso = await _service.Desativar(id);

        if (!sucesso)
        {
            return CustomResponse(new
            {
                success = false,
                errors = new List<string> {"Não foi possível desativar o usuário"}
            });
        }

        return CustomResponse();
    }
}