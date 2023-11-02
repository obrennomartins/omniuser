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

        builder.Property(usuario => usuario.Nome)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(usuario => usuario.Email)
            .HasColumnType("text");

        builder.Property(usuario => usuario.Telefone)
            .HasColumnType("text");

        builder.Property(usuario => usuario.Documento)
            .HasColumnType("text");

        builder.Property(usuario => usuario.Ativo)
            .IsRequired()
            .HasColumnType("boolean");

        builder.Property(usuario => usuario.CriadoEm)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(usuario => usuario.AtualizadoEm)
            .IsRequired()
            .HasColumnType("timestamp");
    }
}