using Bogus;
using OmniUser.Domain.Models;

namespace OmniUser.Tests.Fixtures;

public class EnderecoTestsFixture
{
    public Endereco GerarEndereco()
    {
        var usuario = new UsuarioTestsFixture().GerarUsuario();

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

    public Endereco GerarEnderecoInvalido_LogradouroVazio()
    {
        var endereco = GerarEndereco();

        endereco.Logradouro = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_LogradouroMenor2()
    {
        var endereco = GerarEndereco();

        endereco.Logradouro = endereco.Logradouro[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_LogradouroMaior200()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Logradouro = faker.Random.String2(201);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_NumeroVazio()
    {
        var endereco = GerarEndereco();

        endereco.Numero = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_NumeroMaior20()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Numero = faker.Random.String2(21, 30);

        return endereco;
    }

    public Endereco GerarEnderecoValido_ComplementoNulo()
    {
        var endereco = GerarEndereco();

        endereco.Complemento = null;

        return endereco;
    }

    public Endereco GerarEnderecoValido_ComplementoVazio()
    {
        var endereco = GerarEndereco();

        endereco.Complemento = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_ComplementoMaior100()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Complemento = faker.Random.String2(101);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepVazio()
    {
        var endereco = GerarEndereco();

        endereco.Cep = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepMenor8()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Cep = faker.Random.Digits(1, 0, 7).ToString() ?? "123456";

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepMaior8()
    {
        var endereco = GerarEndereco();

        endereco.Cep += "0";

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CepLetras()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Cep = faker.Random.String2(8);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_BairroVazio()
    {
        var endereco = GerarEndereco();

        endereco.Bairro = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_BairroMenor2()
    {
        var endereco = GerarEndereco();

        endereco.Bairro = endereco.Bairro[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_BairroMaior50()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Bairro = faker.Random.String2(51);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CidadeVazio()
    {
        var endereco = GerarEndereco();

        endereco.Cidade = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CidadeMenor2()
    {
        var endereco = GerarEndereco();

        endereco.Cidade = endereco.Cidade[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_CidadeMaior50()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.Cidade = faker.Random.String2(51);

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_UFVazia()
    {
        var endereco = GerarEndereco();

        endereco.UF = string.Empty;

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_UFMenor2()
    {
        var endereco = GerarEndereco();

        endereco.UF = endereco.UF[..1];

        return endereco;
    }

    public Endereco GerarEnderecoInvalido_UFMaior20()
    {
        var faker = new Faker();
        var endereco = GerarEndereco();

        endereco.UF = faker.Random.String2(21);

        return endereco;
    }
}