﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OmniUser.Infrastructure.Context;

#nullable disable

namespace OmniUser.Infrastructure.Migrations
{
    [DbContext(typeof(OmniUserDbContext))]
    [Migration("20241012181652_RemoveRegistroAuditoria")]
    partial class RemoveRegistroAuditoria
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OmniUser.Domain.Models.Endereco", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("text");

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("text");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .HasMaxLength(100)
                        .HasColumnType("text");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("timestamp");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("text");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("text");

                    b.Property<string>("Uf")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("text");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("OmniUser.Domain.Models.Usuario", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("AtualizadoEm")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("CriadoEm")
                        .HasColumnType("timestamp");

                    b.Property<string>("Documento")
                        .HasMaxLength(20)
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("text");

                    b.Property<string>("Telefone")
                        .HasMaxLength(11)
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("OmniUser.Domain.Models.Endereco", b =>
                {
                    b.HasOne("OmniUser.Domain.Models.Usuario", "Usuario")
                        .WithOne("Endereco")
                        .HasForeignKey("OmniUser.Domain.Models.Endereco", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("OmniUser.Domain.Models.Usuario", b =>
                {
                    b.Navigation("Endereco");
                });
#pragma warning restore 612, 618
        }
    }
}
