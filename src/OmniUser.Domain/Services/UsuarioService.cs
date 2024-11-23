using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.Domain.Services;

public sealed class UsuarioService : BaseService, IUsuarioService
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IViaCepService _viaCepService;
    private bool _disposed;

    public UsuarioService(
        IEnderecoRepository enderecoRepository,
        INotificador notificador,
        IUsuarioRepository usuarioRepository,
        IViaCepService viaCepService) : base(notificador)
    {
        _enderecoRepository = enderecoRepository;
        _usuarioRepository = usuarioRepository;
        _viaCepService = viaCepService;
    }

    public async Task<Usuario?> Obter(int idUsuario, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.Obter(idUsuario, cancellationToken);
        var endereco = await _enderecoRepository.ObterPorUsuarioId(idUsuario, cancellationToken);
        
        if (usuario is null)
        {
            Notificar("Não foi possível obter o usuário.");
            return null;
        }

        usuario.Endereco = endereco;

        return usuario;
    }

    public async Task<Usuario?> Adicionar(Usuario usuario, CancellationToken cancellationToken = default)
    {
        if (!usuario.EhValido())
        {
            return null;
        }

        // Para adicionar o endereço, use o método AdicionarEndereco
        if (usuario.Endereco is not null)
        {
            Notificar("O endereço não pode ser adicionado através deste método.");
            return null;
        }

        var usuarioUnico = await VerificaUnicidade(usuario, cancellationToken);
        if (!usuarioUnico)
        {
            return null;
        }

        var usuarioDb = await _usuarioRepository.Adicionar(usuario, cancellationToken);

        if (usuarioDb is null)
        {
            Notificar("Houve um problema ao salvar o usuário. Tente novamente mais tarde.");
            return null;
        }
    
        return usuarioDb;
    }

    public async Task<Usuario?> Atualizar(Usuario usuario, CancellationToken cancellationToken = default)
    {
        if (!usuario.EhValido())
        {
            return null;
        }

        // Para modificar o endereço, use o método AtualizarEndereco
        if (usuario.Endereco is not null)
        {
            Notificar("O endereço não pode ser modificado através deste método.");
            return null;
        }

        var usuarioDb = await _usuarioRepository.Obter(usuario.Id, cancellationToken);
        if (usuarioDb is null)
        {
            Notificar("Usuário não encontrado.");
            return null;
        }

        // Para modificar o campo Ativo, use os métodos Ativar e Desativar.
        if (usuario.Ativo is not null && usuario.Ativo != usuarioDb.Ativo)
        {
            Notificar("O campo Ativo não pode ser modificado através deste método.");
            return null;
        }

        var usuarioEhUnico = await VerificaUnicidade(usuario, cancellationToken);
        if (!usuarioEhUnico)
        {
            return null;
        }

        usuarioDb.Nome = usuario.Nome;
        usuarioDb.Documento = usuario.Documento;
        usuarioDb.Email = usuario.Email;
        usuarioDb.Telefone = usuario.Telefone;

        var usuarioAtualizado = await _usuarioRepository.Atualizar(usuarioDb, cancellationToken);
        return usuarioAtualizado;
    }

    public async Task<bool> Ativar(int id, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.Obter(id, cancellationToken);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return false;
        }

        if (usuario.Ativo ?? true)
        {
            Notificar("O Usuário já está ativo.");
            return false;
        }

        usuario.Ativo = true;

        return await _usuarioRepository.Atualizar(usuario, cancellationToken) is not null;
    }

    public async Task<bool> Desativar(int id, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioRepository.Obter(id, cancellationToken);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return false;
        }

        if (!(usuario.Ativo ?? true))
        {
            Notificar("O Usuário já está desativado.");
            return false;
        }

        usuario.Ativo = false;
        var usuarioAtualizado = await _usuarioRepository.Atualizar(usuario, cancellationToken) is not null;

        return usuarioAtualizado;
    }

    // TODO
    #region Endereço

    public async Task<Endereco?> AdicionarEndereco(Endereco endereco, CancellationToken cancellationToken = default)
    {
        var cepEhValido = await CepValido(endereco);
        if (!endereco.EhValido() || !cepEhValido)
        {
            return null;
        }

        var usuarioDb = await _usuarioRepository.Obter(endereco.UsuarioId, cancellationToken);
        if (usuarioDb is null)
        {
            Notificar("Não foi possível obter o usuário associado ao endereço.");
            return null;
        }

        var existeEnderecoParaUsuario = false;

        usuarioDb.Endereco = await _enderecoRepository.Adicionar(endereco, cancellationToken);

        await _usuarioRepository.Atualizar(usuarioDb, cancellationToken);

        return endereco;
    }

    public async Task<Endereco?> AtualizarEndereço(Endereco endereco, CancellationToken cancellationToken = default)
    {
        var cepEhValido = await CepValido(endereco);
        if (!endereco.EhValido() || !cepEhValido)
        {
            return null;
        }

        var enderecoDb = await _enderecoRepository.ObterPorUsuarioId(endereco.UsuarioId, cancellationToken);
        if (enderecoDb is null)
        {
            Notificar("Não existe endereço cadastrado para o usuário. Cadastre um endereço antes de atualizá-lo.");
            return null;
        }

        enderecoDb.Logradouro = endereco.Logradouro;
        enderecoDb.Numero = endereco.Numero;
        enderecoDb.Complemento = endereco.Numero;
        enderecoDb.Cep = endereco.Cep;
        enderecoDb.Bairro = endereco.Bairro;
        enderecoDb.Cidade = endereco.Cidade;
        enderecoDb.Uf = endereco.Uf;

        return await _enderecoRepository.Atualizar(enderecoDb, cancellationToken);
    }

    #endregion

    // TODO
    /// <summary>
    /// Verifica se o documento, o e-mail e o telefone do <see cref="Usuario" /> são únicos. Se não forem, notifica.
    /// </summary>
    /// <param name="usuario">O Usuário a ser verificado.</param>
    /// <returns>Retorna true se ele for único, false caso contrário.</returns>
    private async Task<bool> VerificaUnicidade(Usuario usuario, CancellationToken cancellationToken = default)
    {
        var duplicidadeUsuarioDto = await _usuarioRepository.VerificarDuplicidade(
            usuario.Email,
            usuario.Telefone,
            usuario.Documento,
            cancellationToken
        );

        if (duplicidadeUsuarioDto is null or { EmailExistente: false, TelefoneExistente: false, DocumentoExistente: false})
        {
            return true;
        }

        if (duplicidadeUsuarioDto.EmailExistente)
        {
            Notificar("Já existe um usuário com o e-mail informado.");
        }

        if (duplicidadeUsuarioDto.TelefoneExistente)
        {
            Notificar("Já existe um usuário com o telefone informado.");
        }

        if (duplicidadeUsuarioDto.DocumentoExistente)
        {
            Notificar("Já existe um usuário com o documento informado.");
        }

        return false;
    }

    // TODO
    /// <summary>
    /// Verifica se o <see cref="Endereco"> 
    /// </summary>
    private async Task<bool> CepValido(Endereco endereco)
    {
        var enderecoDaApi = await _viaCepService.ObterEndereco(endereco.Cep);

        if (enderecoDaApi == null)
        {
            return false;
        }

        if (endereco.Uf != enderecoDaApi.Uf)
        {
            Notificar(
                $"A UF informada não corresponde ao CEP. " +
                $"UF informada: {endereco.Uf}. " +
                $"UF do CEP: {enderecoDaApi.Uf}.");
            return false;
        }

        if (endereco.Cidade != enderecoDaApi.Localidade)
        {
            Notificar(
                $"A cidade informada não corresponde ao CEP. " +
                $"Cidade informada: {endereco.Cidade}. " +
                $"Cidade do CEP: {enderecoDaApi.Localidade}.");
            return false;
        }

        return true;
    }

    ~UsuarioService()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            _enderecoRepository.Dispose();
            _usuarioRepository.Dispose();
        }

        _disposed = true;
    }
}
