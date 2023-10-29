using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Context;

public class OmniUserContext : DbContext
{
    public OmniUserContext(DbContextOptions<OmniUserContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = default!;
    public DbSet<RegistroAuditoria> RegistrosAuditoria { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniUserContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entidadesModificadas = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .ToList();
    
        foreach (var registroAuditoria in entidadesModificadas.Select(entidadeModificada => new RegistroAuditoria
                 {
                     Entidade = entidadeModificada.Entity.GetType().Name,
                     Acao = entidadeModificada.State.ToString(),
                     Timestamp = DateTime.Now,
                     Alteracoes = GetChanges(entidadeModificada)
                 }))
        {
            RegistrosAuditoria.Add(registroAuditoria);
        }
    
        return base.SaveChangesAsync(cancellationToken);
    }

    private static string GetChanges(EntityEntry entity)
    {
        var changes = new Dictionary<string, object>();

        foreach (var property in entity.OriginalValues.Properties)
        {
            var original = entity.OriginalValues[property];
            var current = entity.CurrentValues[property];

            if (entity.State is EntityState.Added)
            {
                changes[property.Name] = original ?? string.Empty;
            }
            else if (!Equals(original, current))
            {
                changes[property.Name] = new Dictionary<string, string>
                {
                    {"From", $"{original}"},
                    {"To", $"{current}"}
                };
            }
        }

        return JsonSerializer.Serialize(changes);
    }
}