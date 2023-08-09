using OmniUser.Tests.Fixtures;

namespace OmniUser.Tests.Models;

[CollectionDefinition(nameof(UsuarioCollection))]
public class UsuarioCollection : ICollectionFixture<UsuarioTestsFixture>
{
}

[Collection(nameof(UsuarioCollection))]
public class UsuarioTests
{
    private readonly UsuarioTestsFixture _usuarioTestsFixture;

    public UsuarioTests(UsuarioTestsFixture usuarioTestsFixture)
    {
        _usuarioTestsFixture = usuarioTestsFixture;
    }

    [Fact(DisplayName = "Novo Usuario válido")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuario_DeveEstarValido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuario();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.True(valido);
        Assert.Empty(erros);
    }

    [Fact(DisplayName = "Novo Usuario inválido (Nome vazio)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioNomeVazio_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_NomeVazio();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Nome precisa ter entre 2 e 100 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Usuario inválido (Nome < 2)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioNomeMenor2_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_NomeMenor2();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Nome precisa ter entre 2 e 100 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Usuario inválido (Nome > 100)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioNomeMaior100_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_NomeMaior100();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Nome precisa ter entre 2 e 100 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Usuário válido (Email nulo)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioEmailNulo_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioValido_EmailNulo();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.True(valido);
        Assert.Empty(erros);
    }

    [Fact(DisplayName = "Novo Usuário inválido (Email vazio)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioEmailVazio_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_EmailVazio();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Email precisa ser um e-mail válido", erros);
    }

    [Fact(DisplayName = "Novo Usuário inválido (Email inválido)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioEmailInvalido_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_EmailInvalido();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Email precisa ser um e-mail válido", erros);
    }

    [Fact(DisplayName = "Novo Usuário inválido (Email > 100)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioEmailMaior100_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_EmailMaior100();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Email precisa ter no máximo 100 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Usuário válido (Telefone nulo)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioTelefoneNulo_DeveEstarValido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioValido_TelefoneNulo();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.True(valido);
        Assert.Empty(erros);
    }

    [Fact(DisplayName = "Novo Usuário inválido (Telefone vazio)")]
    [Trait("Categoria", "Model - Usuario")]
    public void Usuario_NovoUsuarioTelefoneVazio_DeveEstarInvalido()
    {
        // Arrange
        var usuario = _usuarioTestsFixture.GerarUsuarioInvalido_TelefoneVazio();

        // Act
        var valido = usuario.EhValido();
        var erros = usuario.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToList();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Telefone precisa ter os 2 dígitos do DDD e mais 8 ou 9 dígitos do telefone", erros);
    }
}