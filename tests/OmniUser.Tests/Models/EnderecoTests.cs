using OmniUser.Tests.Fixtures;

namespace OmniUser.Tests.Models;

[CollectionDefinition(nameof(EnderecoCollection))]
public class EnderecoCollection : ICollectionFixture<EnderecoTestsFixture>
{
}

[Collection(nameof(EnderecoCollection))]
public class EnderecoTests
{
    private readonly EnderecoTestsFixture _enderecoTestsFixture;

    public EnderecoTests(EnderecoTestsFixture enderecoTestsFixture)
    {
        _enderecoTestsFixture = enderecoTestsFixture;
    }

    [Fact(DisplayName = "Novo Endereco Válido")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEndereco_DeveEstarValido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEndereco();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.True(valido);
        Assert.Empty(erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Logradouro Vazio)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoLogradouroVazio_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_LogradouroVazio();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Logradouro precisa ter entre 2 e 200 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Logradouro < 2)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoLogradouroMenor2_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_LogradouroMenor2();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Logradouro precisa ter entre 2 e 200 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Logradouro > 200)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoLogradouroMaior100_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_LogradouroMaior200();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Logradouro precisa ter entre 2 e 200 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Numero Vazio)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoNumeroVazio_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_NumeroVazio();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Numero precisa ter entre 1 e 20 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Numero > 20)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoNumeroMaior20_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_NumeroMaior20();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Numero precisa ter entre 1 e 20 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco válido (Complemento nulo)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoComplementoNulo_DeveEstarValido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoValido_ComplementoNulo();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.True(valido);
        Assert.Empty(erros);
    }

    [Fact(DisplayName = "Novo Endereço válido (Complemento vazio)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoComplementoVazio_DeveEstarValido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoValido_ComplementoVazio();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.True(valido);
        Assert.Empty(erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Complemento > 100)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoComplementoMaior100_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_ComplementoMaior100();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Complemento precisa ter no máximo 100 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (CEP vazio)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCepVazio_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CepVazio();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cep precisa ter 8 dígitos", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (CEP < 8)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCepMenor8_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CepMenor8();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cep precisa ter 8 dígitos", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (CEP > 8)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCepMaior8_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CepMaior8();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cep precisa ter 8 dígitos", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (CEP com letras)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCepLetras_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CepLetras();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cep precisa ter 8 dígitos", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Bairro Vazio)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoBairroVazio_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_BairroVazio();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Bairro precisa ter entre 2 e 50 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Bairro < 2)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoBairroMenor2_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_BairroMenor2();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Bairro precisa ter entre 2 e 50 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Bairro > 50)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoBairroMaior50_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_BairroMaior50();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Bairro precisa ter entre 2 e 50 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Cidade vazio)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCidadeVazio_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CidadeVazio();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cidade precisa ter entre 2 e 50 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Cidade < 2)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCidadeMenor2_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CidadeMenor2();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cidade precisa ter entre 2 e 50 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (Cidade > 50)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoCidadeMaior50_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_CidadeMaior50();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo Cidade precisa ter entre 2 e 50 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (UF vazia)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoUFVazia_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_UFVazia();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo UF precisa ter entre 2 e 20 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (UF < 2)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoUFMenor2_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_UFMenor2();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo UF precisa ter entre 2 e 20 caracteres", erros);
    }

    [Fact(DisplayName = "Novo Endereco inválido (UF > 20)")]
    [Trait("Categoria", "Model - Endereco")]
    public void Endereco_NovoEnderecoUFMaior20_DeveEstarInvalido()
    {
        // Arrange
        var endereco = _enderecoTestsFixture.GerarEnderecoInvalido_UFMaior20();

        // Act
        var valido = endereco.EhValido();
        var erros = endereco.ValidationResult.Errors.Select(validation => validation.ErrorMessage).ToArray();

        // Assert
        Assert.False(valido);
        Assert.Single(erros);
        Assert.Contains("O campo UF precisa ter entre 2 e 20 caracteres", erros);
    }
}