using Bogus;
using OmniUser.Domain.Models;

namespace OmniUser.Tests.Fixtures;

public class EnderecoTestsFixture
{
    public Endereco GerarEnderecoValido()
    {
        var usuario = new UsuarioTestsFixture().GerarUsuarioValido();

        var endereco = new Faker<Endereco>("pt_BR")
            .CustomInstantiator(faker => new Endereco(
                    usuario.Id,
                    faker.Address.StreetName(),
                    faker.Address.BuildingNumber(),
                    faker.Address.SecondaryAddress(),
                    new string(faker.Address.ZipCode().Where(char.IsDigit).ToArray()),
                    faker.Address.County(),
                    faker.Address.City(),
                    faker.Address.State(),
                    usuario
                )
            ).Generate();

        return endereco;
    }

    public Endereco GerarEnderecoInvalido()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Logradouro = string.Empty;
        endereco.Numero = string.Empty;
        endereco.Complemento = faker.Random.String2(101);
        endereco.Cep = string.Empty;
        endereco.Bairro = string.Empty;
        endereco.Cidade = string.Empty;
        endereco.Uf = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_LogradouroVazio()
    {
        var endereco = GerarEnderecoValido();

        endereco.Logradouro = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_LogradouroMenor2()
    {
        var endereco = GerarEnderecoValido();

        endereco.Logradouro = endereco.Logradouro[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_LogradouroMaior200()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Logradouro = faker.Random.String2(201);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_NumeroVazio()
    {
        var endereco = GerarEnderecoValido();

        endereco.Numero = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_NumeroMaior20()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Numero = faker.Random.String2(21, 30);

        return endereco;
    }

    public Endereco GerarEnderecoValido_ComplementoNulo()
    {
        var endereco = GerarEnderecoValido();

        endereco.Complemento = null;

        return endereco;
    }

    public Endereco GerarEnderecoValido_ComplementoVazio()
    {
        var endereco = GerarEnderecoValido();

        endereco.Complemento = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_ComplementoMaior100()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Complemento = faker.Random.String2(101);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepVazio()
    {
        var endereco = GerarEnderecoValido();

        endereco.Cep = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepMenor8()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Cep = faker.Random.Digits(1, 0, 7).ToString() ?? "123456";

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepMaior8()
    {
        var endereco = GerarEnderecoValido();

        endereco.Cep += "0";

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepLetras()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Cep = faker.Random.String2(8);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_BairroVazio()
    {
        var endereco = GerarEnderecoValido();

        endereco.Bairro = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_BairroMenor2()
    {
        var endereco = GerarEnderecoValido();

        endereco.Bairro = endereco.Bairro[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_BairroMaior50()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Bairro = faker.Random.String2(51);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CidadeVazio()
    {
        var endereco = GerarEnderecoValido();

        endereco.Cidade = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CidadeMenor2()
    {
        var endereco = GerarEnderecoValido();

        endereco.Cidade = endereco.Cidade[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CidadeMaior50()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Cidade = faker.Random.String2(51);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_UFVazia()
    {
        var endereco = GerarEnderecoValido();

        endereco.Uf = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_UFMenor2()
    {
        var endereco = GerarEnderecoValido();

        endereco.Uf = endereco.Uf[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_UFMaior20()
    {
        var faker = new Faker();
        var endereco = GerarEnderecoValido();

        endereco.Uf = faker.Random.String2(21);

        return endereco;
    }
}
