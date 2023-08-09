using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.HasKey(usuario => usuario.Id);

        builder.HasOne(usuario => usuario.Endereco)
            .WithOne(endereco => endereco.Usuario)
            .HasForeignKey<Endereco>(endereco => endereco.UsuarioId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}