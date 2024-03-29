using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Models.Validations;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.Domain.Services;

public sealed class UsuarioService : BaseService, IUsuarioService
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IViaCepService _viaCepService;
    private bool _disposed;

    public UsuarioService(INotificador notificador,
        IEnderecoRepository enderecoRepository,
        IUsuarioRepository usuarioRepository,
        IViaCepService viaCepService) : base(notificador)
    {
        _enderecoRepository = enderecoRepository;
        _usuarioRepository = usuarioRepository;
        _viaCepService = viaCepService;
    }

    public async Task<Usuario?> Adicionar(Usuario usuario)
    {
        if (!ExecutarValidacao(new UsuarioValidation(), usuario))
        {
            return null;
        }

        if (usuario.Endereco is not null && (!ExecutarValidacao(new EnderecoValidation(), usuario.Endereco) ||
                                             !CepValido(usuario.Endereco).Result))
        {
            return null;
        }

        if (!VerificaUnicidade(usuario))
        {
            return null;
        }

        return await _usuarioRepository.Adicionar(usuario);
    }

    public async Task<Usuario?> Atualizar(Usuario usuario)
    {
        if (!usuario.EhValido() ||
            (usuario.Endereco != null && !usuario.Endereco.EhValido()))
        {
            return null;
        }

        var usuarioDb = await _usuarioRepository.Obter(usuario.Id);
        if (usuarioDb == null)
        {
            Notificar("Usuário não encontrado.");
            return null;
        }

        // Para modificar o campo Ativo, use os métodos Ativar e Desativar.
        if (usuarioDb.Ativo != usuario.Ativo)
        {
            Notificar("O campo Ativo não deve ser modificado através deste método.");
            return null;
        }

        if (!VerificaUnicidade(usuario)) return null;

        usuarioDb.Nome = usuario.Nome;
        usuarioDb.Documento = usuario.Documento;
        usuarioDb.Email = usuario.Email;
        usuarioDb.Telefone = usuario.Telefone;

        return await _usuarioRepository.Atualizar(usuarioDb);
    }

    public async Task<bool> Ativar(int id)
    {
        var usuario = await _usuarioRepository.Obter(id);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return false;
        }

        if (usuario.Ativo)
        {
            Notificar("O Usuário já está ativo.");
            return false;
        }

        usuario.Ativo = true;

        return await _usuarioRepository.Atualizar(usuario) is not null;
    }

    public async Task<bool> Desativar(int id)
    {
        var usuario = await _usuarioRepository.Obter(id);

        if (usuario == null)
        {
            Notificar("Usuário não encontrado.");
            return false;
        }

        if (!usuario.Ativo)
        {
            Notificar("O Usuário já está desativado.");
            return false;
        }

        usuario.Ativo = false;
        return await _usuarioRepository.Atualizar(usuario) is not null;
    }

    public async Task<Endereco?> AtualizarEndereço(Endereco endereco)
    {
        if (!endereco.EhValido())
        {
            return null;
        }

        return await _enderecoRepository.Atualizar(endereco);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UsuarioService()
    {
        Dispose();
    }

    /// <summary>
    ///     Verifica se o documento, o e-mail e o telefone do <see cref="Usuario" /> são únicos. Se não forem, notifica.
    /// </summary>
    /// <param name="usuario">O Usuário a ser verificado.</param>
    /// <returns>Retorna true se ele for único, false caso contrário.</returns>
    private bool VerificaUnicidade(Usuario usuario)
    {
        var usuarioExistente = _usuarioRepository.Buscar(u =>
            (usuario.Documento != null && u.Documento == usuario.Documento) ||
            (usuario.Email != null && u.Email == usuario.Email) ||
            (usuario.Telefone != null && u.Telefone == usuario.Telefone)).Result.FirstOrDefault();

        // ReSharper disable once InvertIf
        if (usuarioExistente is not null)
        {
            if (usuario.Documento is not null && usuarioExistente.Documento == usuario.Documento)
                Notificar("Já existe um usuário com o documento informado.");
            else if (usuario.Email is not null && usuarioExistente.Email == usuario.Email)
                Notificar("Já existe um usuário com o e-mail informado.");
            else if (usuario.Telefone is not null && usuarioExistente.Telefone == usuario.Telefone)
                Notificar("Já existe um usuário com o telefone informado.");

            return false;
        }

        return true;
    }

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
                $"A Uf informada não corresponde ao CEP. " +
                $"Uf informada: {endereco.Uf}. " +
                $"Uf do CEP: {enderecoDaApi.Uf}.");
            return false;
        }

        // ReSharper disable once InvertIf
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