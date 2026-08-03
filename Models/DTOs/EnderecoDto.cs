using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models.DTOs
{
    public class EnderecoDto
    {
        public int EnderecoId { get; set; }
        public Estado Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
    }
}
