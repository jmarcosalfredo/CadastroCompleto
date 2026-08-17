using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CadastroCompleto.Models.Base;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models
{
    public class Endereco : BaseEntity
    {
        public int EnderecoId { get; set; }
        public Estado Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        // Propriedade de navegação para o Cliente (1:1)
        public Cliente Cliente { get; set; }
        // Chave estrangeira para o Cliente (Esta aqui pq endereco é dependente de cliente)
        public int ClienteId { get; set; }
    }
}
