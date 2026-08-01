using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CadastroCompleto.Migrations
{
    /// <inheritdoc />
    public partial class AddTelefonesEMocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telefone",
                columns: table => new
                {
                    TelefoneId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Ddd = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Numero = table.Column<string>(type: "character varying(9)", maxLength: 9, nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefone", x => x.TelefoneId);
                    table.ForeignKey(
                        name: "FK_Telefone_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Telefone",
                columns: new[] { "TelefoneId", "ClienteId", "Ddd", "Numero", "Tipo" },
                values: new object[,]
                {
                    { 1, 1, "11", "987654321", 0 },
                    { 2, 1, "11", "33221100", 2 },
                    { 3, 1, "11", "32109876", 1 },
                    { 4, 2, "21", "976543210", 0 },
                    { 5, 3, "31", "965432109", 0 },
                    { 6, 3, "31", "32211009", 1 },
                    { 7, 4, "51", "954321098", 0 },
                    { 8, 5, "41", "943210987", 0 },
                    { 9, 5, "41", "31100998", 2 },
                    { 10, 5, "41", "30099887", 1 },
                    { 11, 6, "48", "932109876", 0 },
                    { 12, 6, "48", "30988776", 2 },
                    { 13, 7, "71", "921098765", 0 },
                    { 14, 8, "62", "910987654", 0 },
                    { 15, 8, "62", "30877665", 2 },
                    { 16, 8, "62", "32166554", 1 },
                    { 17, 9, "61", "909876543", 0 },
                    { 18, 9, "61", "32055443", 1 },
                    { 19, 10, "85", "898765432", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Telefone_ClienteId",
                table: "Telefone",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telefone");
        }
    }
}
