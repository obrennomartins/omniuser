using Microsoft.EntityFrameworkCore;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Context;

public class OmniUserContext : DbContext
{
    public OmniUserContext(DbContextOptions<OmniUserContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OmniUserContext).Assembly);
    }
}