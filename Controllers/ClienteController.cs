using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Models.DTOs;
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
        public async Task<ActionResult<List<ClienteDto>>> GetAll()
        {
            var clientes = await _clienteServices.FindAllAsync();

            return Ok(clientes.Adapt<List<ClienteDto>>());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetById(int id)
        {
            var cliente = await _clienteServices.FindByIdAsync(id);

            if (cliente == null)
                return NotFound();

            return Ok(cliente.Adapt<ClienteDto>());
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Create(ClienteDto clienteDto)
        {
            var cliente = clienteDto.Adapt<Cliente>();
            var novoCliente = await _clienteServices.CreateAsync(cliente);

            return Ok(novoCliente.Adapt<ClienteDto>());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> Update(int id, ClienteDto clienteDto)
        {
            if (id != clienteDto.ClienteId)
            {
                return BadRequest();
            }

            var cliente = clienteDto.Adapt<Cliente>();
            var clienteAtualizado = await _clienteServices.UpdateAsync(cliente);

            if (clienteAtualizado == null)
            {
                return NotFound();
            }

            return Ok(clienteAtualizado.Adapt<ClienteDto>());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cliente = await _clienteServices.FindByIdAsync(id);
            if (cliente == null)
                return NotFound();

            await _clienteServices.DeleteAsync(id);
            return NoContent();
        }
    }
}
