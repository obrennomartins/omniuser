using Dapper;
using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Infrastructure.Session;

namespace OmniUser.Infrastructure.Repositories;

public sealed class EnderecoRepository : IEnderecoRepository
{
    private readonly OmniUserDbSession _session;
    private bool _disposed;

    public EnderecoRepository(OmniUserDbSession session)
    {
        _session = session;
    }

    public async Task<Endereco?> Adicionar(Endereco endereco, CancellationToken cancellationToken = default)
    {
        const string query = @"
            INSERT INTO `Enderecos` (`UsuarioId`, `Logradouro`, `Numero`, `Complemento`, `Cep`, `Bairro`, `Cidade`, `Uf`, `CriadoEm`, `AtualizadoEm`)
            VALUES (@UsuarioId, @Logradouro, @Numero, @Complemento, @Cep, @Bairro, @Cidade, @Uf, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP);

            SELECT * FROM `Enderecos` WHERE `Id` = LAST_INSERT_ID(); 
        ";

        var parameters = new DynamicParameters(new {
            endereco.UsuarioId,
            endereco.Logradouro,
            endereco.Numero,
            endereco.Complemento,
            endereco.Cep,
            endereco.Bairro,
            endereco.Cidade,
            endereco.Uf
        });

        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var multi = await _session.Connection. QueryMultipleAsync(command);
        var enderecoAdicionado = await multi.ReadSingleOrDefaultAsync<Endereco>();

        return enderecoAdicionado;
    }

    public async Task<Endereco?> Atualizar(Endereco endereco, CancellationToken cancellationToken = default)
    {
        const string query = @"
            UPDATE `Enderecos`
            SET
                Logradouro = @Logradouro,
                Numero = @Numero,
                Complemento = @Complemento,
                Cep = @Cep,
                Bairro = @Bairro,
                Cidade = @Cidade,
                Uf = @Uf,
                AtualizadoEm = CURRENT_TIMESTAMP
            WHERE Id = @Id;

            SELECT * FROM `Enderecos` WHERE Id = @Id;
        ";

        var parameters = new DynamicParameters(new {
            endereco.Id,
            endereco.Logradouro,
            endereco.Numero,
            endereco.Complemento,
            endereco.Cep,
            endereco.Bairro,
            endereco.Cidade,
            endereco.Uf
        });

        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var multi = await _session.Connection.QueryMultipleAsync(command);
        var enderecoAtualizado = await multi.ReadSingleOrDefaultAsync<Endereco>();

        return enderecoAtualizado;
    }

    public async Task<Endereco?> Obter(int id, CancellationToken cancellationToken = default)
    {
        const string query = "SELECT * FROM `Enderecos` WHERE Id = @Id;";
        var parameters = new DynamicParameters(new { id });
        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var endereco = await _session.Connection.QueryFirstOrDefaultAsync<Endereco>(command);

        return endereco;
    }

    public async Task<Endereco?> ObterPorUsuarioId(int usuarioId, CancellationToken cancellationToken = default)
    {

        const string query = "SELECT * FROM `Enderecos` WHERE UsuarioId = @UsuarioId;";
        var parameters = new DynamicParameters(new { usuarioId });
        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var endereco = await _session.Connection.QueryFirstOrDefaultAsync<Endereco>(command);

        return endereco;
    }

    public async Task<bool?> VerificarExistenciaPorUsuarioId(int usuarioId, CancellationToken cancellationToken = default)
    {
        return null;
    }

    public async Task<bool> Remover(int id, CancellationToken cancellationToken = default)
    {
        const string query = "DELETE FROM `Usuarios` WHERE Id = @Id;";
        var parameters = new DynamicParameters(new { id });
        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var linhasAfetadas = await _session.Connection.ExecuteAsync(command);

        return linhasAfetadas == 1;
    }

    public async Task<bool> RemoverPorUsuarioId(int usuarioId, CancellationToken cancellationToken = default)
    {
        const string query = "DELETE FROM `Usuarios` WHERE UsuarioId = @UsuarioId;";
        var parameters = new DynamicParameters(new { usuarioId });
        var command = new CommandDefinition(query, parameters, _session.Transaction, cancellationToken: cancellationToken);
        var linhasAfetadas = await _session.Connection.ExecuteAsync(command);

        return linhasAfetadas == 1;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~EnderecoRepository()
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
