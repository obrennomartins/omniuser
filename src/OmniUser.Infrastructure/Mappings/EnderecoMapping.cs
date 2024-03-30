using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Mappings;

public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.HasKey(endereco => endereco.Id);

        builder.HasOne(endereco => endereco.Usuario)
            .WithOne(usuario => usuario.Endereco)
            .HasForeignKey<Endereco>(endereco => endereco.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(endereco => endereco.Logradouro)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(200);

        builder.Property(endereco => endereco.Numero)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(20);

        builder.Property(endereco => endereco.Complemento)
            .HasColumnType("text")
            .HasMaxLength(100);

        builder.Property(endereco => endereco.Cep)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(8);

        builder.Property(endereco => endereco.Bairro)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(50);

        builder.Property(endereco => endereco.Cidade)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(50);

        builder.Property(endereco => endereco.Uf)
            .IsRequired()
            .HasColumnType("text")
            .HasMaxLength(20);

        builder.Property(endereco => endereco.CriadoEm)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(endereco => endereco.AtualizadoEm)
            .IsRequired()
            .HasColumnType("timestamp");
    }
}
