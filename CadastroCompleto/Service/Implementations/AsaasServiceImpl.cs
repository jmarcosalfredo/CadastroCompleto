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

        public async Task<AsaasBillingResponseDto> CreateBillingAsync(AsaasBillingRequestDto billingRequest)
        {
            var httpClient = _httpClientFactory.CreateClient("Asaas");

            var request = new
            {
                customer = billingRequest.Customer,
                billingType = billingRequest.BillingType.ToString(),
                value = billingRequest.Value,
                dueDate = billingRequest.DueDate.ToString("yyyy-MM-dd"),

                description = billingRequest.Description,
                externalReference = billingRequest.ExternalReference,

                discount = billingRequest.Discount == null ? null : new
                {
                    value = billingRequest.Discount.Value,
                    dueDateLimitDays = billingRequest.Discount.DueDateLimitDays,
                    type = billingRequest.Discount.Type.ToString()
                },

                fine = billingRequest.Fine == null ? null : new
                {
                    value = billingRequest.Fine.Value,
                    type = billingRequest.Fine.Type.ToString()
                },

                interest = billingRequest.Interest == null ? null : new
                {
                    value = billingRequest.Interest.Value
                }
            };

            try
            {
                var response = await httpClient.PostAsJsonAsync("payments", request);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Status Code: {response.StatusCode}. Detalhes: {errorBody}");
                }

                return await response.Content.ReadFromJsonAsync<AsaasBillingResponseDto>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro inesperado ao criar cobrança no Asaas: {ex.Message}", ex);
            }
        }
    }
}
