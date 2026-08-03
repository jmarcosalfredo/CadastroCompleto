using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models.DTOs
{
    public class ClienteDto
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

        public EnderecoDto Endereco { get; set; }
        public List<TelefoneDto> Telefones { get; set; } = new List<TelefoneDto>();
    }
}
