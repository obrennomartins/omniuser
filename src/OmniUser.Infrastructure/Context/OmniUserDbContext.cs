using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Context;

public class OmniUserDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public OmniUserDbContext(DbContextOptions<OmniUserDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    static OmniUserDbContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<Usuario> Usuarios { get; set; } = default!;
    public DbSet<RegistroAuditoria> RegistrosAuditoria { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder
            .UseMySql(
                _configuration.GetConnectionString("MYSQLCONNSTR_localdb"),
                ServerVersion.AutoDetect(_configuration.GetConnectionString("MYSQLCONNSTR_localdb")),
                builder =>
                {
                    builder.MigrationsHistoryTable(_configuration.GetConnectionString("MySQLPrefix") + "__EFMigrationsHistory");
                });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniUserDbContext).Assembly);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(_configuration.GetConnectionString("MySQLPrefix") + "_" + entity.GetTableName());
        }
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

        foreach (var registroAuditoria in entityEntries.Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted).Select(entidadeModificada => new RegistroAuditoria
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
                changes[property.Name] = new Dictionary<string, object?>
                {
                    {"From", original},
                    {"To", current}
                };
            }
        }

        changes["Id"] = entity.OriginalValues.GetValue<int>("Id");

        var retorno = JsonSerializer.Serialize(changes);
        return retorno;
    }
}