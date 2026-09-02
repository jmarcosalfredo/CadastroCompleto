using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CadastroCompleto.Migrations
{
    /// <inheritdoc />
    public partial class AddOutbox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefone_Clientes_ClienteId",
                table: "Telefone");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefone",
                table: "Telefone");

            migrationBuilder.RenameTable(
                name: "Telefone",
                newName: "Telefones");

            migrationBuilder.RenameIndex(
                name: "IX_Telefone_ClienteId",
                table: "Telefones",
                newName: "IX_Telefones_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones",
                column: "TelefoneId");

            migrationBuilder.CreateTable(
                name: "Outboxes",
                columns: table => new
                {
                    OutboxId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    CriadoEm = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    ProcessadoEm = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    Tentativas = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    UltimoErro = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outboxes", x => x.OutboxId);
                    table.ForeignKey(
                        name: "FK_Outboxes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Outboxes_ClienteId",
                table: "Outboxes",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefones_Clientes_ClienteId",
                table: "Telefones");

            migrationBuilder.DropTable(
                name: "Outboxes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Telefones",
                table: "Telefones");

            migrationBuilder.RenameTable(
                name: "Telefones",
                newName: "Telefone");

            migrationBuilder.RenameIndex(
                name: "IX_Telefones_ClienteId",
                table: "Telefone",
                newName: "IX_Telefone_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Telefone",
                table: "Telefone",
                column: "TelefoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefone_Clientes_ClienteId",
                table: "Telefone",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "ClienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
