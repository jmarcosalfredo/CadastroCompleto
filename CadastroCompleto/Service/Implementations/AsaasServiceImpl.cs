using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.DTOs.Asaas;

namespace CadastroCompleto.Service.Implementations
{
    public class AsaasServiceImpl : IAsaasService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AsaasServiceImpl(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<AsaasResponseDto> CreateCustumerAsync(Cliente cliente)
        {
            var httpClient = _httpClientFactory.CreateClient("Asaas");

            var celular = cliente.Telefones.FirstOrDefault();

            var request = new AsaasRequestDto
            {
                Name = cliente.NomeCompleto,
                CpfCnpj = cliente.Cpf,
                Email = cliente.Email,
                PostalCode = cliente.Endereco?.Cep
            };

            try
            {
                var response = await httpClient.PostAsJsonAsync("customers", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Status Code: {response.StatusCode}. Detalhes: {errorBody}");
                }

                return await response.Content.ReadFromJsonAsync<AsaasResponseDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado ao criar cliente no Asaas: {ex.Message}", ex);
            }
        }
    }
}
