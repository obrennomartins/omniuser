using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OmniUser.API.Controllers;
using OmniUser.API.ViewModels;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.Tests.Controller;

public class UsuariosControllerTests
{
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly Mock<IUsuarioService> _mockUsuarioService;
    private readonly Mock<INotificador> _mockNotificador;
    private readonly Mock<IMapper> _mockMapper;
    private readonly UsuariosController _controller;

    public UsuariosControllerTests()
    {
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockUsuarioService = new Mock<IUsuarioService>();
        _mockNotificador = new Mock<INotificador>();
        _mockMapper = new Mock<IMapper>();
        _controller = new UsuariosController(
            _mockMapper.Object,
            _mockNotificador.Object,
            _mockUsuarioRepository.Object,
            _mockUsuarioService.Object);
    }

    [Fact]
    public async Task Index_ActionExecutes_ReturnsViewForIndex()
    {
        var result = await _controller.ObterTodosAtivos();

        Assert.IsType<ActionResult<List<UsuarioViewModel>>>(result);
    }
}
