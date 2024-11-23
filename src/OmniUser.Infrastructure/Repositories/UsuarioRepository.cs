using System.Text;
using Dapper;
using OmniUser.Domain.Dtos;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Infrastructure.Session;

namespace OmniUser.Infrastructure.Repositories;

public sealed class UsuarioRepository : IUsuarioRepository
{
    private readonly OmniUserDbSession _session;
    private bool _disposed;

    public UsuarioRepository(OmniUserDbSession session)
    {
        _session = session;
    }

    public async Task<Usuario?> Adicionar(Usuario usuario) => await Adicionar(usuario, CancellationToken.None);

    public async Task<Usuario?> Adicionar(Usuario usuario, CancellationToken cancellationToken = default)
    {
        const string query = @"
            INSERT INTO `Usuarios` (`Nome`, `Email`, `Telefone`, `Documento`, `Ativo`, `CriadoEm`, `AtualizadoEm`)
            VALUES (@Nome, @Email, @Telefone, @Documento, 1, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);
            
            SELECT * FROM Usuarios WHERE Id = LAST_INSERT_ID();
        ";

        var parameters = new DynamicParameters(new {
            usuario.Nome,
            usuario.Email,
            usuario.Telefone,
            usuario.Documento
        });

        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var multi = await _session.Connection.QueryMultipleAsync(command);
        var usuarioAdicionado = await multi.ReadSingleOrDefaultAsync<Usuario>();

        return usuarioAdicionado;
    }

    public async Task<Usuario?> Atualizar(Usuario usuario, CancellationToken cancellationToken = default)
    {
        const string query = @"
            UPDATE `Usuarios`
            SET
                Nome = @Nome,
                Email = @Email,
                Telefone = @Telefone,
                Documento = @Documento,
                Ativo = @Ativo,
                AtualizadoEm = CURRENT_TIMESTAMP
            WHERE Id = @Id;

            SELECT * FROM Usuarios WHERE Id = @Id;
        ";

        var parameters = new DynamicParameters(new {
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.Telefone,
            usuario.Documento,
            usuario.Ativo
        });

        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var multi = await _session.Connection.QueryMultipleAsync(command);
        var usuarioAtualizado = await multi.ReadSingleOrDefaultAsync<Usuario>();

        return usuarioAtualizado;
    }

    public async Task<Usuario?> Obter(int id, CancellationToken cancellationToken = default)
    {
        const string query = "SELECT * FROM Usuarios WHERE Id = @Id;";
        var parameters = new DynamicParameters(new { id });
        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var usuario = await _session.Connection.QueryFirstOrDefaultAsync<Usuario>(command);

        return usuario;
    }

    public async Task<IEnumerable<Usuario?>> ObterTodosAtivos(CancellationToken cancellationToken = default)
    {
        const string query = "SELECT * FROM Usuarios WHERE Ativo = 1;";
        var command = new CommandDefinition(query, transaction: _session.Transaction, cancellationToken: cancellationToken);
        var usuarios = await _session.Connection.QueryAsync<Usuario>(command);

        return usuarios;
    }

    public async Task<IEnumerable<Usuario?>> ObterTodosInativos(CancellationToken cancellationToken = default)
    {
        const string query = "SELECT * FROM Usuarios WHERE Ativo = 0;";

        var command = new CommandDefinition(query, null, _session.Transaction, cancellationToken: cancellationToken);
        var usuarios = await _session.Connection.QueryAsync<Usuario>(command);

        return usuarios;
    }

    public async Task<DuplicidadeUsuarioDto?> VerificarDuplicidade(string? email = null, string? telefone = null, string? documento = null, CancellationToken cancellationToken = default)
    {
        var query = new StringBuilder("SELECT ");
        var parameters = new DynamicParameters();

        if (!string.IsNullOrEmpty(email))
        {
            query.Append("EXISTS(SELECT 1 FROM `Usuarios` WHERE `Email` = @Email) AS EmailExistente, ");
            parameters.Add("Email", email);
        }
        else
        {
            query.Append("false AS EmailExistente, ");
        }

        if (!string.IsNullOrEmpty(telefone))
        {
            query.Append("EXISTS(SELECT 1 FROM `Usuarios` WHERE `Telefone` = @Telefone) AS TelefoneExistente, ");
            parameters.Add("Telefone", telefone);
        }
        else
        {
            query.Append("false AS TelefoneExistente, ");
        }

        if (!string.IsNullOrEmpty(documento))
        {
            query.Append("EXISTS(SELECT 1 FROM `Usuarios` WHERE `Documento` = @Documento) AS DocumentoExistente ");
            parameters.Add("Documento", documento);
        }
        else
        {
            query.Append("false AS DocumentoExistente ");
        }

        query.Append("FROM `Usuarios` LIMIT 1;");

        var command = new CommandDefinition(query.ToString(), parameters, _session.Transaction, cancellationToken: cancellationToken);
        var duplicidadeUsuarioDto = await _session.Connection.QueryFirstOrDefaultAsync<DuplicidadeUsuarioDto>(command);

        return duplicidadeUsuarioDto;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UsuarioRepository()
    {
        Dispose();
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _session.Dispose();
        }

        _disposed = true;
    }
}
