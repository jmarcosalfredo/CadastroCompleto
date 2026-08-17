using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.DTOs;
using CadastroCompleto.Models.Responses;
using CadastroCompleto.Service;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CadastroCompleto.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private IClienteServices _clienteServices;

        public ClienteController(IClienteServices clienteServices)
        {
            _clienteServices = clienteServices;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ClienteDto>>>> GetAll()
        {
            var response = await _clienteServices.FindAllAsync();

            var result = new ServiceResponse<List<ClienteDto>>
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem,
                Dados = response.Sucesso ? response.Dados.Adapt<List<ClienteDto>>() : null
            };

            return response.Sucesso ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ClienteDto>>> GetById(int id)
        {
            var response = await _clienteServices.FindByIdAsync(id);

            var result = new ServiceResponse<ClienteDto>
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem,
                Dados = response.Sucesso ? response.Dados.Adapt<ClienteDto>() : null
            };

            return response.Sucesso ? Ok(result) : BadRequest(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ClienteDto>>> Create(ClienteDto clienteDto)
        {
            var cliente = clienteDto.Adapt<Cliente>();
            var response = await _clienteServices.CreateAsync(cliente);

            var result = new ServiceResponse<ClienteDto>
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem,
                Dados = response.Sucesso ? response.Dados.Adapt<ClienteDto>() : null
            };

            return response.Sucesso ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ServiceResponse<ClienteDto>>> Update(int id, ClienteDto clienteDto)
        {
            if (id != clienteDto.ClienteId)
            {
                return BadRequest(new ServiceResponse<ClienteDto>
                {
                    Sucesso = false,
                    Mensagem = "Os ids devem ser compativeis!"
                });
            }

            var cliente = clienteDto.Adapt<Cliente>();
            var response = await _clienteServices.UpdateAsync(cliente);

            var result = new ServiceResponse<ClienteDto>
            {
                Sucesso = response.Sucesso,
                Mensagem = response.Mensagem,
                Dados = response.Sucesso ? response.Dados.Adapt<ClienteDto>() : null
            };

            return response.Sucesso ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<bool>>> Delete(int id)
        {
            var response = await _clienteServices.DeleteAsync(id);

            return response.Sucesso ? NoContent() : NotFound(response);
        }
    }
}
