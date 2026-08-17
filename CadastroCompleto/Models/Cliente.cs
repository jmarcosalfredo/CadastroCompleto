using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Base;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models
{
    public class Cliente : BaseEntity
    {
        public int ClienteId { get; set; }
        public string NomeCompleto { get; set; }
        public DateOnly DataNascimento { get; set; }
        public string Nacionalidade { get; set; }
        public EstadoCivil EstadoCivil { get; set; }
        public string RgOuCin { get; set; }
        public Estado OrgaoExpedidor { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTimeOffset DataCriacao { get; set; }

        // Propriedade de navegação para Endereco do cliente (1:1)
        public Endereco Endereco { get; set; }

        // Propriedade de navegação para Telefone do cliente (1:N)
        public ICollection<Telefone> Telefones { get; set; } = new List<Telefone>();
    }
}
