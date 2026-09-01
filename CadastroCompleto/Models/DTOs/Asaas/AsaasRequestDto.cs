using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CadastroCompleto.Models.DTOs.Asaas
{
    public class AsaasRequestDto
    {

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cpfCnpj")]
        public string CpfCnpj { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

    }
}

