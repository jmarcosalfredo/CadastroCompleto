using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.Base;

namespace CadastroCompleto.Models
{
    public class Outbox : BaseEntity
    {
        public int OutboxId { get; set; }
        public string Tipo { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTimeOffset CriadoEm { get; set; }
        public DateTimeOffset? ProcessadoEm { get; set; }
        public int Tentativas { get; set; }
        public string UltimoErro { get; set; }
    }
}
