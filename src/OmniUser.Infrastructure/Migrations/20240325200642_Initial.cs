using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OmniUser.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegistrosAuditoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Usuario = table.Column<string>(type: "text", nullable: true),
                    Entidade = table.Column<string>(type: "text", nullable: false),
                    Acao = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Alteracoes = table.Column<string>(type: "text", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosAuditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "text", maxLength: 100, nullable: true),
                    Telefone = table.Column<string>(type: "text", maxLength: 11, nullable: true),
                    Documento = table.Column<string>(type: "text", maxLength: 20, nullable: true),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Logradouro = table.Column<string>(type: "text", maxLength: 200, nullable: false),
                    Numero = table.Column<string>(type: "text", maxLength: 20, nullable: false),
                    Complemento = table.Column<string>(type: "text", maxLength: 100, nullable: true),
                    Cep = table.Column<string>(type: "text", maxLength: 8, nullable: false),
                    Bairro = table.Column<string>(type: "text", maxLength: 50, nullable: false),
                    Cidade = table.Column<string>(type: "text", maxLength: 50, nullable: false),
                    Uf = table.Column<string>(type: "text", maxLength: 20, nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "timestamp", nullable: false),
                    AtualizadoEm = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_UsuarioId",
                table: "Endereco",
                column: "UsuarioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.DropTable(
                name: "RegistrosAuditoria");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
