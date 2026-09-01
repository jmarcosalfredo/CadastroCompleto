using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.DTOs.Asaas;

namespace CadastroCompleto.Service
{
    public interface IAsaasService
    {
        Task<AsaasResponseDto> CreateCustumerAsync(Cliente cliente);
    }
}
