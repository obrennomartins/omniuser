using Bogus;
using Bogus.Extensions.Brazil;
using OmniUser.Domain.Models;

namespace OmniUser.Tests.Fixtures;

public sealed class UsuarioTestsFixture : IDisposable
{
    public Usuario GerarUsuarioValido()
    {
        var usuario = new Faker<Usuario>("pt_BR")
            .CustomInstantiator(faker => new Usuario(
                faker.Name.FirstName(),
                "",
                new string(faker.Phone.PhoneNumber().Replace("+55", "").Where(char.IsDigit).ToArray()),
                new string(faker.Person.Cpf().Where(char.IsDigit).ToArray()),
                null,
                true
            )).RuleFor(u => u.Email, (f, u) =>
                f.Internet.Email(u.Nome.ToLower()))
            .Generate();

        return usuario;
    }

    public Usuario GerarUsuarioInvalido()
    {
        var usuario = GerarUsuarioValido();

        usuario.Nome = string.Empty;
        usuario.Email = null;
        usuario.Telefone = null;
        usuario.Documento = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_NomeVazio()
    {
        var usuario = GerarUsuarioValido();
        usuario.Nome = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_NomeMenor2()
    {
        var usuario = GerarUsuarioValido();
        usuario.Nome = usuario.Nome[..1];

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_NomeMaior100()
    {
        var usuario = GerarUsuarioValido();
        usuario.Nome = new Faker().Random.String2(101);

        return usuario;
    }

    public Usuario GerarUsuarioValido_EmailNulo()
    {
        var usuario = GerarUsuarioValido();
        usuario.Email = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_EmailVazio()
    {
        var usuario = GerarUsuarioValido();
        usuario.Email = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_EmailInvalido()
    {
        var usuario = GerarUsuarioValido();
        usuario.Email = "emailInvalido";

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_EmailMaior100()
    {
        var usuario = GerarUsuarioValido();
        usuario.Email = new Faker().Random.String2(101) + usuario.Email;

        return usuario;
    }

    public Usuario GerarUsuarioValido_TelefoneNulo()
    {
        var usuario = GerarUsuarioValido();
        usuario.Telefone = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_TelefoneVazio()
    {
        var usuario = GerarUsuarioValido();
        usuario.Telefone = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_TelefoneMenor10()
    {
        var usuario = GerarUsuarioValido();
        usuario.Telefone = usuario.Telefone?[..9];

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_TelefoneMaior11()
    {
        var usuario = GerarUsuarioValido();
        usuario.Telefone = new Faker().Random.String2(12, "1234567890");

        return usuario;
    }

    public Usuario GerarUsuarioValido_DocumentoNulo()
    {
        var usuario = GerarUsuarioValido();
        usuario.Documento = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_DocumentoVazio()
    {
        var usuario = GerarUsuarioValido();
        usuario.Documento = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_DocumentoLetras()
    {
        var usuario = GerarUsuarioValido();
        usuario.Documento += "a";

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_DocumentoMaior20()
    {
        var usuario = GerarUsuarioValido();
        usuario.Documento = new Faker().Random.String2(21, "1234567890");

        return usuario;
    }

    ~UsuarioTestsFixture()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
