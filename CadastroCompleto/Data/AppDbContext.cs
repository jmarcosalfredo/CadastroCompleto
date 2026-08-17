using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace CadastroCompleto.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.NomeCompleto)
                .HasMaxLength(200)
                .IsRequired();

                entity.Property(e => e.DataNascimento)
                .IsRequired();

                entity.Property(e => e.Nacionalidade)
                .IsRequired();

                entity.Property(e => e.EstadoCivil)
                .IsRequired();

                entity.Property(e => e.RgOuCin)
                .HasMaxLength(20)
                .IsRequired();

                entity.Property(e => e.OrgaoExpedidor)
                .IsRequired();

                entity.Property(e => e.Cpf)
                .HasMaxLength(14)
                .IsRequired();

                entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(e => e.DataCriacao)
                .HasDefaultValueSql("now()")
                .IsRequired();

                entity.HasOne(e => e.Endereco)
                .WithOne(c => c.Cliente)
                .HasForeignKey<Endereco>(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                            new Cliente { ClienteId = 1, NomeCompleto = "João Pedro Almeida", DataNascimento = new DateOnly(1990, 3, 15), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Solteiro, RgOuCin = "12.345.678-9", OrgaoExpedidor = Estado.SP, Cpf = "123.456.789-01", Email = "joao.almeida@email.com", DataCriacao = new DateTimeOffset(2024, 1, 10, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 2, NomeCompleto = "Maria Fernanda Costa", DataNascimento = new DateOnly(1985, 7, 22), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Casado, RgOuCin = "23.456.789-0", OrgaoExpedidor = Estado.RJ, Cpf = "234.567.890-12", Email = "maria.costa@email.com", DataCriacao = new DateTimeOffset(2024, 1, 11, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 3, NomeCompleto = "Carlos Eduardo Souza", DataNascimento = new DateOnly(1978, 11, 3), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Divorciado, RgOuCin = "34.567.890-1", OrgaoExpedidor = Estado.MG, Cpf = "345.678.901-23", Email = "carlos.souza@email.com", DataCriacao = new DateTimeOffset(2024, 1, 12, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 4, NomeCompleto = "Ana Beatriz Lima", DataNascimento = new DateOnly(1995, 1, 30), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Solteiro, RgOuCin = "45.678.901-2", OrgaoExpedidor = Estado.RS, Cpf = "456.789.012-34", Email = "ana.lima@email.com", DataCriacao = new DateTimeOffset(2024, 1, 13, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 5, NomeCompleto = "Pedro Henrique Rocha", DataNascimento = new DateOnly(1988, 9, 17), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Casado, RgOuCin = "56.789.012-3", OrgaoExpedidor = Estado.PR, Cpf = "567.890.123-45", Email = "pedro.rocha@email.com", DataCriacao = new DateTimeOffset(2024, 1, 14, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 6, NomeCompleto = "Juliana Martins Alves", DataNascimento = new DateOnly(1992, 4, 8), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Solteiro, RgOuCin = "67.890.123-4", OrgaoExpedidor = Estado.SC, Cpf = "678.901.234-56", Email = "juliana.alves@email.com", DataCriacao = new DateTimeOffset(2024, 1, 15, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 7, NomeCompleto = "Rafael Nascimento Dias", DataNascimento = new DateOnly(1980, 6, 25), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Viuvo, RgOuCin = "78.901.234-5", OrgaoExpedidor = Estado.BA, Cpf = "789.012.345-67", Email = "rafael.dias@email.com", DataCriacao = new DateTimeOffset(2024, 1, 16, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 8, NomeCompleto = "Fernanda Oliveira Santos", DataNascimento = new DateOnly(1997, 12, 5), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Solteiro, RgOuCin = "89.012.345-6", OrgaoExpedidor = Estado.GO, Cpf = "890.123.456-78", Email = "fernanda.santos@email.com", DataCriacao = new DateTimeOffset(2024, 1, 17, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 9, NomeCompleto = "Lucas Gabriel Ferreira", DataNascimento = new DateOnly(1983, 2, 14), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Casado, RgOuCin = "90.123.456-7", OrgaoExpedidor = Estado.DF, Cpf = "901.234.567-89", Email = "lucas.ferreira@email.com", DataCriacao = new DateTimeOffset(2024, 1, 18, 9, 0, 0, TimeSpan.Zero) },
                            new Cliente { ClienteId = 10, NomeCompleto = "Camila Ribeiro Torres", DataNascimento = new DateOnly(1991, 10, 19), Nacionalidade = "Brasileira", EstadoCivil = EstadoCivil.Divorciado, RgOuCin = "01.234.567-8", OrgaoExpedidor = Estado.CE, Cpf = "012.345.678-90", Email = "camila.torres@email.com", DataCriacao = new DateTimeOffset(2024, 1, 19, 9, 0, 0, TimeSpan.Zero) }
                            );
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.Property(e => e.Estado)
                .IsRequired();

                entity.Property(e => e.Cidade)
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(e => e.Bairro)
                .HasMaxLength(100)
                .IsRequired();

                entity.Property(e => e.Rua)
                .HasMaxLength(200)
                .IsRequired();

                entity.Property(e => e.Numero)
                .IsRequired();

                entity.Property(e => e.Complemento)
                .HasMaxLength(100);

                entity.Property(e => e.Cep)
                .HasMaxLength(15)
                .IsRequired();

                entity.HasData(
                        new Endereco { EnderecoId = 1, ClienteId = 1, Estado = Estado.SP, Cidade = "São Paulo", Bairro = "Centro", Rua = "Rua das Flores", Numero = 120, Complemento = "Apto 32", Cep = "01000-000" },
                        new Endereco { EnderecoId = 2, ClienteId = 2, Estado = Estado.RJ, Cidade = "Rio de Janeiro", Bairro = "Copacabana", Rua = "Av. Atlântica", Numero = 850, Complemento = null, Cep = "22010-000" },
                        new Endereco { EnderecoId = 3, ClienteId = 3, Estado = Estado.MG, Cidade = "Belo Horizonte", Bairro = "Savassi", Rua = "Rua Pernambuco", Numero = 45, Complemento = "Casa", Cep = "30130-150" },
                        new Endereco { EnderecoId = 4, ClienteId = 4, Estado = Estado.RS, Cidade = "Porto Alegre", Bairro = "Moinhos de Vento", Rua = "Rua Padre Chagas", Numero = 210, Complemento = "Bloco B", Cep = "90570-080" },
                        new Endereco { EnderecoId = 5, ClienteId = 5, Estado = Estado.PR, Cidade = "Curitiba", Bairro = "Batel", Rua = "Av. do Batel", Numero = 1500, Complemento = "Sala 12", Cep = "80420-090" },
                        new Endereco { EnderecoId = 6, ClienteId = 6, Estado = Estado.SC, Cidade = "Florianópolis", Bairro = "Centro", Rua = "Rua Felipe Schmidt", Numero = 320, Complemento = null, Cep = "88010-000" },
                        new Endereco { EnderecoId = 7, ClienteId = 7, Estado = Estado.BA, Cidade = "Salvador", Bairro = "Barra", Rua = "Av. Oceânica", Numero = 780, Complemento = "Apto 501", Cep = "40140-130" },
                        new Endereco { EnderecoId = 8, ClienteId = 8, Estado = Estado.GO, Cidade = "Goiânia", Bairro = "Setor Bueno", Rua = "Rua T-30", Numero = 655, Complemento = "Casa 2", Cep = "74223-060" },
                        new Endereco { EnderecoId = 9, ClienteId = 9, Estado = Estado.DF, Cidade = "Brasília", Bairro = "Asa Sul", Rua = "SQS 108", Numero = 12, Complemento = "Bloco C", Cep = "70347-000" },
                        new Endereco { EnderecoId = 10, ClienteId = 10, Estado = Estado.CE, Cidade = "Fortaleza", Bairro = "Meireles", Rua = "Av. Beira Mar", Numero = 990, Complemento = "Apto 1201", Cep = "60165-121" }
                        );
            });

            modelBuilder.Entity<Telefone>(entity =>
            {
                entity.Property(e => e.Tipo)
                .IsRequired();

                entity.Property(e => e.Ddd)
                .HasMaxLength(3)
                .IsRequired();

                entity.Property(e => e.Numero)
                .HasMaxLength(9)
                .IsRequired();

                entity.HasOne(e => e.Cliente)
                .WithMany(c => c.Telefones)
                .HasForeignKey(t => t.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasData(
                    new Telefone { TelefoneId = 1, Tipo = TelefoneTipo.Celular, Ddd = "11", Numero = "987654321", ClienteId = 1 },
                    new Telefone { TelefoneId = 2, Tipo = TelefoneTipo.Residencial, Ddd = "11", Numero = "33221100", ClienteId = 1 },
                    new Telefone { TelefoneId = 3, Tipo = TelefoneTipo.Comercial, Ddd = "11", Numero = "32109876", ClienteId = 1 },

                    new Telefone { TelefoneId = 4, Tipo = TelefoneTipo.Celular, Ddd = "21", Numero = "976543210", ClienteId = 2 },

                    new Telefone { TelefoneId = 5, Tipo = TelefoneTipo.Celular, Ddd = "31", Numero = "965432109", ClienteId = 3 },
                    new Telefone { TelefoneId = 6, Tipo = TelefoneTipo.Comercial, Ddd = "31", Numero = "32211009", ClienteId = 3 },

                    new Telefone { TelefoneId = 7, Tipo = TelefoneTipo.Celular, Ddd = "51", Numero = "954321098", ClienteId = 4 },

                    new Telefone { TelefoneId = 8, Tipo = TelefoneTipo.Celular, Ddd = "41", Numero = "943210987", ClienteId = 5 },
                    new Telefone { TelefoneId = 9, Tipo = TelefoneTipo.Residencial, Ddd = "41", Numero = "31100998", ClienteId = 5 },
                    new Telefone { TelefoneId = 10, Tipo = TelefoneTipo.Comercial, Ddd = "41", Numero = "30099887", ClienteId = 5 },

                    new Telefone { TelefoneId = 11, Tipo = TelefoneTipo.Celular, Ddd = "48", Numero = "932109876", ClienteId = 6 },
                    new Telefone { TelefoneId = 12, Tipo = TelefoneTipo.Residencial, Ddd = "48", Numero = "30988776", ClienteId = 6 },

                    new Telefone { TelefoneId = 13, Tipo = TelefoneTipo.Celular, Ddd = "71", Numero = "921098765", ClienteId = 7 },

                    new Telefone { TelefoneId = 14, Tipo = TelefoneTipo.Celular, Ddd = "62", Numero = "910987654", ClienteId = 8 },
                    new Telefone { TelefoneId = 15, Tipo = TelefoneTipo.Residencial, Ddd = "62", Numero = "30877665", ClienteId = 8 },
                    new Telefone { TelefoneId = 16, Tipo = TelefoneTipo.Comercial, Ddd = "62", Numero = "32166554", ClienteId = 8 },


                    new Telefone { TelefoneId = 17, Tipo = TelefoneTipo.Celular, Ddd = "61", Numero = "909876543", ClienteId = 9 },
                    new Telefone { TelefoneId = 18, Tipo = TelefoneTipo.Comercial, Ddd = "61", Numero = "32055443", ClienteId = 9 },

                    new Telefone { TelefoneId = 19, Tipo = TelefoneTipo.Celular, Ddd = "85", Numero = "898765432", ClienteId = 10 }
                    );
            });
        }
    }
}
