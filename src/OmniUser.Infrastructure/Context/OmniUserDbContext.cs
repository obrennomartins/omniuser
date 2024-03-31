using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Context;

public class OmniUserDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    static OmniUserDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public OmniUserDbContext(DbContextOptions<OmniUserDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Usuario> Usuarios { get; init; } = default!;
    public DbSet<RegistroAuditoria> RegistrosAuditoria { get; init; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["POSTGRESQLCONNSTR_OmniUserDb"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniUserDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entityEntries = ChangeTracker.Entries().ToList();

        foreach (var entry in entityEntries.Where(entry => entry.Entity.GetType().GetProperty("CriadoEm") != null))
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property("CriadoEm").CurrentValue = DateTime.Now;
            }
        }

        foreach (var registroAuditoria in entityEntries
                     .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
                     .Select(entidadeModificada => new RegistroAuditoria
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

    private static string GetChanges(EntityEntry entidade)
    {
        var alteracoes = new Dictionary<string, object>();

        foreach (var propriedade in entidade.OriginalValues.Properties)
        {
            var original = entidade.OriginalValues[propriedade];
            var atual = entidade.CurrentValues[propriedade];

            if (entidade.State is EntityState.Added)
            {
                alteracoes[propriedade.Name] = original ?? string.Empty;
            }
            else if (!Equals(original, atual))
            {
                alteracoes[propriedade.Name] = new Dictionary<string, object?>
                {
                    { "De", original },
                    { "Para", atual }
                };
            }
        }

        alteracoes["Id"] = entidade.OriginalValues.GetValue<int>("Id");

        var retorno = JsonSerializer.Serialize(alteracoes);
        return retorno;
    }
}
