using OmniUser.Domain.Models;

namespace OmniUser.Domain.Interfaces;

public interface IRegistroAuditoriaRepository : IDisposable
{
    Task<RegistroAuditoria?> Obter(int id);
    Task<IEnumerable<RegistroAuditoria?>> ObterTodos();
}
