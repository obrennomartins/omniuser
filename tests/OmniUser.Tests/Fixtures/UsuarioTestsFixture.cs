using Bogus;
using Bogus.Extensions.Brazil;
using OmniUser.Domain.Models;

namespace OmniUser.Tests.Fixtures;

public class UsuarioTestsFixture : IDisposable
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public Usuario GerarUsuario()
    {
        var usuario = new Faker<Usuario>("pt_BR")
            .CustomInstantiator(faker => new Usuario(
                faker.Name.FirstName(),
                "",
                new string(faker.Phone.PhoneNumber().Replace("+55", "").Where(char.IsDigit).ToArray()),
                faker.Person.Cpf(),
                null,
                true
            )).RuleFor(u => u.Email, (f, u) =>
                f.Internet.Email(u.Nome.ToLower()))
            .Generate();

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_NomeVazio()
    {
        var usuario = GerarUsuario();
        usuario.Nome = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_NomeMenor2()
    {
        var usuario = GerarUsuario();
        usuario.Nome = usuario.Nome[..1];

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_NomeMaior100()
    {
        var usuario = GerarUsuario();
        usuario.Nome = new Faker().Random.String2(101);

        return usuario;
    }

    public Usuario GerarUsuarioValido_EmailNulo()
    {
        var usuario = GerarUsuario();
        usuario.Email = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_EmailVazio()
    {
        var usuario = GerarUsuario();
        usuario.Email = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_EmailInvalido()
    {
        var usuario = GerarUsuario();
        usuario.Email = "emailinvalido";

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_EmailMaior100()
    {
        var usuario = GerarUsuario();
        usuario.Email = new Faker().Random.String2(101) + usuario.Email;

        return usuario;
    }

    public Usuario GerarUsuarioValido_TelefoneNulo()
    {
        var usuario = GerarUsuario();
        usuario.Telefone = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_TelefoneVazio()
    {
        var usuario = GerarUsuario();
        usuario.Telefone = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_TelefoneMenor10()
    {
        var usuario = GerarUsuario();
        usuario.Telefone = usuario.Telefone?[..9];

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_TelefoneMaior11()
    {
        var usuario = GerarUsuario();
        usuario.Telefone = new Faker().Random.String2(12);

        return usuario;
    }

    public Usuario GerarUsuarioValido_DocumentoNulo()
    {
        var usuario = GerarUsuario();
        usuario.Documento = null;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_DocumentoVazio()
    {
        var usuario = GerarUsuario();
        usuario.Documento = string.Empty;

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_DocumentoLetras()
    {
        var usuario = GerarUsuario();
        usuario.Documento += "a";

        return usuario;
    }

    public Usuario GerarUsuarioInvalido_DocumentoMaior20()
    {
        var usuario = GerarUsuario();
        usuario.Documento = new Faker().Random.String2(21);

        return usuario;
    }
}