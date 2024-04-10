using Moq;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Notificacoes;
using OmniUser.Domain.Services;

namespace OmniUser.Tests.Services;

public class UsuarioServiceTests
{
    private readonly Mock<INotificador> _mockNotificador;
    private readonly Mock<IEnderecoRepository> _mockEnderecoRepositorio;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepositorio;
    private readonly Mock<IViaCepService> _mockViaCepService;
    private readonly IUsuarioService _usuarioService;

    public UsuarioServiceTests()
    {
        _mockNotificador = new Mock<INotificador>();
        _mockEnderecoRepositorio = new Mock<IEnderecoRepository>();
        _mockUsuarioRepositorio = new Mock<IUsuarioRepository>();
        _mockViaCepService = new Mock<IViaCepService>();

        _usuarioService = new UsuarioService(
            _mockNotificador.Object,
            _mockEnderecoRepositorio.Object,
            _mockUsuarioRepositorio.Object,
            _mockViaCepService.Object);
    }

    #region Usuario.Adicionar
    
    [Fact(DisplayName = "Usuário válido")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarUsuario_QuandoUsuarioValido()
    {
        // Arrange
        var usuario = new Usuario("Abc", null, null, null, null, true);
        _mockUsuarioRepositorio
            .Setup(usuarioRepository => usuarioRepository.Adicionar(usuario))
            .ReturnsAsync(usuario);

        // Act
        var resultado = await _usuarioService.Adicionar(usuario);

        // Assert
        Assert.NotNull(resultado);
        Assert.IsType<Usuario>(resultado);
        
        _mockNotificador.Verify(
            notificador => notificador.Handle(It.IsAny<Notificacao>()),
            Times.Never);
        _mockUsuarioRepositorio.Verify(
            repository => repository.Adicionar(It.IsAny<Usuario>()),
            Times.Once);
    }

    [Fact(DisplayName = "Usuário inválido")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoUsuarioInvalido()
    {
        // Arrange
        var usuarioInvalido = new Usuario("", null, null, null, null, true);

        // Act
        var resultado = await _usuarioService.Adicionar(usuarioInvalido);

        // Assert
        Assert.Null(resultado);
        
        _mockNotificador.Verify(
            notificador => notificador.Handle(It.IsAny<Notificacao>()),
            Times.Once);
        _mockUsuarioRepositorio.Verify(
            repository => repository.Adicionar(It.IsAny<Usuario>()),
            Times.Never);
    }

    [Fact(DisplayName = "Endereço inválido")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoEnderecoInvalido()
    {
        // Arrange
        var endereco = new Endereco(0, "", "", null, "", "", "", "", It.IsAny<Usuario>());
        var usuario = new Usuario("Abc", null, null, null, endereco, true);

        // Act
        var resultado = await _usuarioService.Adicionar(usuario);
        
        // Assert
        Assert.Null(resultado);

        _mockNotificador.Verify(
            notificador => notificador.Handle(It.IsAny<Notificacao>()),
            Times.AtLeastOnce);
        _mockUsuarioRepositorio.Verify(
            repository => repository.Adicionar(It.IsAny<Usuario>()),
            Times.Never);
    }

    [Fact(DisplayName = "CEP inválido - Cidade")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoCepInvalidoCidade()
    {
        
    }

    [Fact(DisplayName = "CEP inválido - UF")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoCepInvalidoUf()
    {
        
    }

    [Fact(DisplayName = "Documento duplicado")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoDocumentoDuplicado()
    {
        
    }

    [Fact(DisplayName = "E-mail duplicado")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoEmailDuplicado()
    {
        
    }

    [Fact(DisplayName = "Telefone duplicado")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoTelefoneDuplicado()
    {
        
    }
    
    #endregion
    
    
}
