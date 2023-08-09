using OmniUser.Domain.Interfaces;
using OmniUser.Domain.Models;
using OmniUser.Domain.Notificacoes;

namespace OmniUser.Domain.Services;

public class UsuarioService : BaseService, IUsuarioService
{
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IViaCepRepository _viaCepRepository;
    private bool _disposed;

    public UsuarioService(INotificador notificador,
        IEnderecoRepository enderecoRepository,
        IUsuarioRepository usuarioRepository,
        IViaCepRepository viaCepRepository) : base(notificador)
    {
        _usuarioRepository = usuarioRepository;
        _viaCepRepository = viaCepRepository;
        _enderecoRepository = enderecoRepository;
    }

    public async Task<Usuario?> Adicionar(Usuario usuario)
    {
        if (!usuario.EhValido())
        {
            return null;
        }

        if (usuario.Endereco == null || !usuario.Endereco.EhValido() || !await CepValido(usuario.Endereco))
        {
            return null;
        }

        // O documento não é obrigatório, mas, se informado, deve ser diferente de qualquer outro usuário
        if (usuario.Documento != null && _usuarioRepository.Buscar(u => u.Documento == usuario.Documento).Result.Any())
        {
            Notificar("Já existe um usuário com o documento informado.");
            return null;
        }

        // O e-mail não é obrigatório, mas, se informado, deve ser diferente de qualquer outro usuário
        if (usuario.Email != null && _usuarioRepository.Buscar(u => u.Email == usuario.Email).Result.Any())
        {
            Notificar("Já existe um usuário com o e-mail informado.");
            return null;
        }

        // O telefone não é obrigatório, mas, se informado, deve ser diferente de qualquer outro usuário
        if (usuario.Telefone != null && _usuarioRepository.Buscar(u => u.Telefone == usuario.Telefone).Result.Any())
        {
            Notificar("Já existe um usuário com o telefone informado.");
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

        if (_usuarioRepository.Buscar(u => u.Documento == usuario.Documento && u.Id != usuario.Id).Result.Any())
        {
            Notificar("Já existe um usuário com o documento informado.");
            return null;
        }

        // O e-mail não é obrigatório, mas, se informado, deve ser diferente de qualquer outro usuário
        if (usuario.Email != null &&
            _usuarioRepository.Buscar(u => u.Email == usuario.Email && u.Id != usuario.Id).Result.Any())
        {
            Notificar("Já existe um usuário com o e-mail informado.");
            return null;
        }

        // O telefone não é obrigatório, mas, se informado, deve ser diferente de qualquer outro usuário
        if (usuario.Telefone != null &&
            _usuarioRepository.Buscar(u => u.Telefone == usuario.Telefone && u.Id != usuario.Id).Result.Any())
        {
            Notificar("Já existe um usuário com o telefone informado.");
            return null;
        }

        // Para modificar o campo Ativo, use os métodos Ativar e Desativar.
        if (usuarioDb.Ativo != usuario.Ativo)
        {
            Notificar("O campo Ativo não deve ser modificado através deste método.");
            return null;
        }

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

    private async Task<bool> CepValido(Endereco endereco)
    {
        var enderecoDaApi = await _viaCepRepository.ObterEndereco(endereco.Cep);

        if (enderecoDaApi == null)
        {
            Notificar("CEP não encontrado.");
            return false;
        }

        if (endereco.UF != enderecoDaApi.Uf)
        {
            Notificar(
                $"A UF informada não corresponde ao CEP. UF informada: {endereco.UF}. UF do CEP: {enderecoDaApi.Uf}.");
            return false;
        }

        if (endereco.Cidade != enderecoDaApi.Localidade)
        {
            Notificar(
                $"A cidade informada não corresponde ao CEP. Cidade informada: {endereco.Cidade}. Cidade do CEP: {enderecoDaApi.Localidade}.");
            return false;
        }

        return true;
    }

    protected virtual void Dispose(bool disposing)
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