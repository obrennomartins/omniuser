using Moq;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Notificacoes;
using OmniUser.Domain.Services;
using OmniUser.Tests.Fixtures;

namespace OmniUser.Tests.Services;

public class UsuarioServiceTests : IClassFixture<EnderecoTestsFixture>, IClassFixture<UsuarioTestsFixture>
{
    private readonly Mock<INotificador> _mockNotificador;
    private readonly Mock<IEnderecoRepository> _mockEnderecoRepositorio;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepositorio;
    private readonly Mock<IViaCepService> _mockViaCepService;
    private readonly EnderecoTestsFixture _enderecoTestsFixture;
    private readonly UsuarioTestsFixture _usuarioTestsFixture;
    private readonly IUsuarioService _usuarioService;

    public UsuarioServiceTests(EnderecoTestsFixture enderecoTestsFixture, UsuarioTestsFixture usuarioTestsFixture)
    {
        _mockNotificador = new Mock<INotificador>();
        _mockEnderecoRepositorio = new Mock<IEnderecoRepository>();
        _mockUsuarioRepositorio = new Mock<IUsuarioRepository>();
        _mockViaCepService = new Mock<IViaCepService>();

        _usuarioTestsFixture = usuarioTestsFixture;
        _enderecoTestsFixture = enderecoTestsFixture;

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
        var usuario = _usuarioTestsFixture.GerarUsuarioValido();
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
        var usuarioInvalido = _usuarioTestsFixture.GerarUsuarioInvalido();

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
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido();
        var usuario = _usuarioTestsFixture.GerarUsuarioValido();
        usuario.Endereco = endereco;

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
        // Arrange

        // Act

        // Assert
    }

    [Fact(DisplayName = "CEP inválido - UF")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoCepInvalidoUf()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(DisplayName = "Documento duplicado")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoDocumentoDuplicado()
    {
        // Arrange
        var usuario1 = _usuarioTestsFixture.GerarUsuarioValido();
        var usuario2 = _usuarioTestsFixture.GerarUsuarioValido();
        usuario2.Documento = usuario1.Documento;

        var resultado1Db = await _usuarioService.Adicionar(usuario1);

        // Act
        var resultado = await _usuarioService.Adicionar(usuario2);
        // TODO

        // Assert
    }

    [Fact(DisplayName = "E-mail duplicado")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoEmailDuplicado()
    {
        // Arrange

        // Act

        // Assert
    }

    [Fact(DisplayName = "Telefone duplicado")]
    [Trait("Category", "Service - Usuario - Adicionar")]
    public async Task Adicionar_DeveRetornarNulo_QuandoTelefoneDuplicado()
    {
        // Arrange

        // Act

        // Assert
    }

    #endregion
}
