using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CadastroCompleto.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaMocks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "ClienteId", "Cpf", "DataCriacao", "DataNascimento", "Email", "EstadoCivil", "Nacionalidade", "NomeCompleto", "OrgaoExpedidor", "RgOuCin" },
                values: new object[,]
                {
                    { 1, "123.456.789-01", new DateTimeOffset(new DateTime(2024, 1, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1990, 3, 15), "joao.almeida@email.com", 0, "Brasileira", "João Pedro Almeida", 24, "12.345.678-9" },
                    { 2, "234.567.890-12", new DateTimeOffset(new DateTime(2024, 1, 11, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1985, 7, 22), "maria.costa@email.com", 1, "Brasileira", "Maria Fernanda Costa", 18, "23.456.789-0" },
                    { 3, "345.678.901-23", new DateTimeOffset(new DateTime(2024, 1, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1978, 11, 3), "carlos.souza@email.com", 2, "Brasileira", "Carlos Eduardo Souza", 12, "34.567.890-1" },
                    { 4, "456.789.012-34", new DateTimeOffset(new DateTime(2024, 1, 13, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1995, 1, 30), "ana.lima@email.com", 0, "Brasileira", "Ana Beatriz Lima", 20, "45.678.901-2" },
                    { 5, "567.890.123-45", new DateTimeOffset(new DateTime(2024, 1, 14, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1988, 9, 17), "pedro.rocha@email.com", 1, "Brasileira", "Pedro Henrique Rocha", 15, "56.789.012-3" },
                    { 6, "678.901.234-56", new DateTimeOffset(new DateTime(2024, 1, 15, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1992, 4, 8), "juliana.alves@email.com", 0, "Brasileira", "Juliana Martins Alves", 23, "67.890.123-4" },
                    { 7, "789.012.345-67", new DateTimeOffset(new DateTime(2024, 1, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1980, 6, 25), "rafael.dias@email.com", 3, "Brasileira", "Rafael Nascimento Dias", 4, "78.901.234-5" },
                    { 8, "890.123.456-78", new DateTimeOffset(new DateTime(2024, 1, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1997, 12, 5), "fernanda.santos@email.com", 0, "Brasileira", "Fernanda Oliveira Santos", 8, "89.012.345-6" },
                    { 9, "901.234.567-89", new DateTimeOffset(new DateTime(2024, 1, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1983, 2, 14), "lucas.ferreira@email.com", 1, "Brasileira", "Lucas Gabriel Ferreira", 6, "90.123.456-7" },
                    { 10, "012.345.678-90", new DateTimeOffset(new DateTime(2024, 1, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateOnly(1991, 10, 19), "camila.torres@email.com", 2, "Brasileira", "Camila Ribeiro Torres", 5, "01.234.567-8" }
                });

            migrationBuilder.InsertData(
                table: "Enderecos",
                columns: new[] { "EnderecoId", "Bairro", "Cep", "Cidade", "ClienteId", "Complemento", "Estado", "Numero", "Rua" },
                values: new object[,]
                {
                    { 1, "Centro", "01000-000", "São Paulo", 1, "Apto 32", 24, 120, "Rua das Flores" },
                    { 2, "Copacabana", "22010-000", "Rio de Janeiro", 2, null, 18, 850, "Av. Atlântica" },
                    { 3, "Savassi", "30130-150", "Belo Horizonte", 3, "Casa", 12, 45, "Rua Pernambuco" },
                    { 4, "Moinhos de Vento", "90570-080", "Porto Alegre", 4, "Bloco B", 20, 210, "Rua Padre Chagas" },
                    { 5, "Batel", "80420-090", "Curitiba", 5, "Sala 12", 15, 1500, "Av. do Batel" },
                    { 6, "Centro", "88010-000", "Florianópolis", 6, null, 23, 320, "Rua Felipe Schmidt" },
                    { 7, "Barra", "40140-130", "Salvador", 7, "Apto 501", 4, 780, "Av. Oceânica" },
                    { 8, "Setor Bueno", "74223-060", "Goiânia", 8, "Casa 2", 8, 655, "Rua T-30" },
                    { 9, "Asa Sul", "70347-000", "Brasília", 9, "Bloco C", 6, 12, "SQS 108" },
                    { 10, "Meireles", "60165-121", "Fortaleza", 10, "Apto 1201", 5, 990, "Av. Beira Mar" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Enderecos",
                keyColumn: "EnderecoId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Clientes",
                keyColumn: "ClienteId",
                keyValue: 10);
        }
    }
}
