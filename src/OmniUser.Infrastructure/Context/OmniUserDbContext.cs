using Microsoft.EntityFrameworkCore;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["POSTGRESQLCONNSTR_OmniUserDb"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniUserDbContext).Assembly);
    }
}
