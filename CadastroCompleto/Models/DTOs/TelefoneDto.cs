using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Enums;

namespace CadastroCompleto.Models.DTOs
{
    public class TelefoneDto
    {
        public int TelefoneId { get; set; }
        public TelefoneTipo Tipo { get; set; }
        public string Ddd { get; set; }
        public string Numero { get; set; }
    }
}
