using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmniUser.Domain.Models;

namespace OmniUser.Infrastructure.Mappings;

public class RegistroAuditoriaMapping : IEntityTypeConfiguration<RegistroAuditoria>
{
    public void Configure(EntityTypeBuilder<RegistroAuditoria> builder)
    {
        builder.HasKey(registro => registro.Id);

        builder.Property(registro => registro.Usuario)
            .HasColumnType("text");

        builder.Property(registro => registro.Entidade)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(registro => registro.Acao)
            .IsRequired()
            .HasColumnType("text");

        builder.Property(registro => registro.Timestamp)
            .IsRequired()
            .HasColumnType("timestamp");

        builder.Property(registro => registro.Alteracoes)
            .IsRequired()
            .HasColumnType("text");
    }
}