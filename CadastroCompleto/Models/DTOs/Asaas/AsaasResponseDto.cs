using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CadastroCompleto.Models.DTOs.Asaas
{
    public class AsaasResponseDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("cpfCnpj")]
        public string CpfCnpj { get; set; }
    }
}
