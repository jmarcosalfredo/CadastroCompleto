using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CadastroCompleto.Models.Base;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models
{
    public class Telefone : BaseEntity
    {
        public int TelefoneId { get; set; }
        public TelefoneTipo Tipo { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }

        // Propriedade de navegação para Cliente (1:N)
        [JsonIgnore]
        public Cliente Cliente { get; set; }
        // Chave estrangeira para o Cliente
        public int ClienteId { get; set; }
    }
}
