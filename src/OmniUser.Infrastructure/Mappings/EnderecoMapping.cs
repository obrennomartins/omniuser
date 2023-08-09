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
    }
}