using CadastroCompleto.Models;
using CadastroCompleto.Models.Enums;
using CadastroCompleto.Repositories;
using CadastroCompleto.Repositories.Implementations;
using CadastroCompleto.Service;
using CadastroCompleto.Service.Implementations;
using FluentAssertions;
using Moq;

namespace CadastroCompleto.Tests
{
    public class ClienteServicesImplTests
    {
        private readonly Mock<IUnitOfWork> _uofMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IRepository<Endereco>> _enderecoRepositoryMock;
        private readonly Mock<IRepository<Telefone>> _telefoneRepositoryMock;
        private readonly IClienteServices _service;

        public ClienteServicesImplTests()
        {
            _uofMock = new Mock<IUnitOfWork>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _enderecoRepositoryMock = new Mock<IRepository<Endereco>>();
            _telefoneRepositoryMock = new Mock<IRepository<Telefone>>();

            _uofMock.Setup(u => u.ClienteRepository).Returns(_clienteRepositoryMock.Object);
            _uofMock.Setup(u => u.EnderecoRepository).Returns(_enderecoRepositoryMock.Object);
            _uofMock.Setup(u => u.TelefoneRepository).Returns(_telefoneRepositoryMock.Object);
            _uofMock.Setup(u => u.CommitAsync()).ReturnsAsync(1);

            _service = new ClienteServicesImpl(_uofMock.Object);
        }

        private static Cliente CriarClienteValido(int clienteId = 1)
        {
            return new Cliente
            {
                ClienteId = clienteId,
                NomeCompleto = "Julio Moreira",
                DataNascimento = new DateOnly(1995, 5, 20),
                Nacionalidade = "Brasileira",
                EstadoCivil = EstadoCivil.Solteiro,
                RgOuCin = "12.345.678-9",
                OrgaoExpedidor = Estado.SP,
                Cpf = "123.456.789-00",
                Email = "julio@teste.com",
                DataCriacao = DateTimeOffset.UtcNow,
                Endereco = new Endereco
                {
                    EnderecoId = clienteId,
                    ClienteId = clienteId,
                    Estado = Estado.SP,
                    Cidade = "Piracicaba",
                    Bairro = "Centro",
                    Rua = "Rua Exemplo",
                    Numero = 100,
                    Complemento = "Apto 1",
                    Cep = "13400-000"
                },
                Telefones = new List<Telefone>
                {
                    new Telefone { TelefoneId = 1, ClienteId = clienteId, Tipo = TelefoneTipo.Celular, Ddd = "19", Numero = "999998888" },
                    new Telefone { TelefoneId = 2, ClienteId = clienteId, Tipo = TelefoneTipo.Comercial, Ddd = "19", Numero = "34349898" }
                }
            };
        }

        [Fact]
        public async Task CreateClienteValido_ShouldReturn_ClienteCriadoComSucesso()
        {
            // Arrange
            var clienteMock = CriarClienteValido();
            _clienteRepositoryMock.Setup(r => r.CreateAsync(clienteMock)).ReturnsAsync(clienteMock);

            // Act
            var result = await _service.CreateAsync(clienteMock);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().Be(clienteMock);
            result.Mensagem.Should().Be("Cliente criado com sucesso!");
            _clienteRepositoryMock.Verify(r => r.CreateAsync(clienteMock), Times.Once);
            _uofMock.Verify(u => u.CommitAsync(), Times.Once);
        }

        [Fact]
        public async Task FindAllAsync_ShouldReturn_ListaClientesComSucesso()
        {
            // Arrange
            var clientesListMock = new List<Cliente> { CriarClienteValido(1), CriarClienteValido(2) };
            _clienteRepositoryMock.Setup(r => r.FindAllAsync()).ReturnsAsync(clientesListMock);

            // Act
            var result = await _service.FindAllAsync();

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().HaveCount(2);
            result.Dados.Should().BeEquivalentTo(clientesListMock);
        }

        [Fact]
        public async Task FindByIdAsync_ShouldReturn_ClienteComSucesso()
        {
            // Arrange
            var clienteMock = CriarClienteValido(10);
            _clienteRepositoryMock.Setup(r => r.FindByIdAsync(5)).ReturnsAsync(clienteMock);

            // Act
            var result = await _service.FindByIdAsync(5);

            // Assert
            result.Sucesso.Should().BeTrue();
            result.Dados.Should().Be(clienteMock);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturn_ClienteComSucesso()
        {
            // Arrange
            var clienteMock = CriarClienteValido(1);
            clienteMock.Telefones.Add(new Telefone { TelefoneId = 3, ClienteId = 1, Tipo = TelefoneTipo.Residencial, Ddd = "19", Numero = "33334444" });

            _clienteRepositoryMock.Setup(r => r.FindByIdAsync(1)).ReturnsAsync(clienteMock);

            var clienteAtualizado = CriarClienteValido(1);
            clienteAtualizado.NomeCompleto = "Luiz Ferreira";
            clienteAtualizado.Telefones.Add(new Telefone { TelefoneId = 0, ClienteId = 1, Tipo = TelefoneTipo.Celular, Ddd = "11", Numero = "988887777" });

            // Act
            var resultado = await _service.UpdateAsync(clienteAtualizado);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Dados.NomeCompleto.Should().Be("Luiz Ferreira");
            _enderecoRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Endereco>()), Times.Once);
            _telefoneRepositoryMock.Verify(r => r.CreateAsync(It.Is<Telefone>(t => t.TelefoneId == 0)), Times.Once);
            _telefoneRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Telefone>(t => t.TelefoneId == 1)), Times.Once);
            _telefoneRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Telefone>(t => t.TelefoneId == 2)), Times.Once);
            _telefoneRepositoryMock.Verify(r => r.DeleteAsync(3), Times.Once);
            _uofMock.Verify(u => u.CommitAsync(), Times.AtLeast(4)); // 1 cliente + 1 endereco + 3 telefones (loop) + 1 delete
        }

        [Fact]
        public async Task DeleteAsync_ClienteExistente_RetornaSucesso()
        {
            // Arrange
            var clienteMock = CriarClienteValido(3);
            _clienteRepositoryMock.Setup(r => r.FindByIdAsync(3)).ReturnsAsync(clienteMock);
            _clienteRepositoryMock.Setup(r => r.DeleteAsync(3)).Returns(Task.CompletedTask);

            // Act
            var resultado = await _service.DeleteAsync(3);

            // Assert
            resultado.Sucesso.Should().BeTrue();
            resultado.Dados.Should().BeTrue();
            _clienteRepositoryMock.Verify(r => r.DeleteAsync(3), Times.Once);
            _uofMock.Verify(u => u.CommitAsync(), Times.Once);
        }
    }
}
