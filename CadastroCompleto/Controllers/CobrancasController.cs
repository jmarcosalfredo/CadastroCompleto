using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models.DTOs.Asaas;
using CadastroCompleto.Models.Responses;
using CadastroCompleto.Service;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCompleto.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CobrancasController : ControllerBase
    {
        private IClienteServices _clienteServices;

        public CobrancasController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        [HttpPost("{clienteId}")]
        public async Task<ActionResult<ServiceResponse<AsaasBillingResponseDto>>> CreateBilling(int clienteId, AsaasBillingRequestDto request)
        {
            var response = await _clienteServices.CreateBillingAsync(clienteId, request);

            var result = new ServiceResponse<AsaasBillingResponseDto>
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem,
                Dados = response.Sucesso ? response.Dados : null
            };

            return response.Sucesso ? Ok(result) : BadRequest(result);
        }
    }
}
