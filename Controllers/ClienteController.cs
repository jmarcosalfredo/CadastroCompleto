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
        public ActionResult<List<ClienteDto>> GetAll()
        {
            var clientes = _clienteServices.FindAll();

            return Ok(clientes.Adapt<List<ClienteDto>>());
        }

        [HttpGet("{id}")]
        public ActionResult<ClienteDto> GetById(int id)
        {
            var cliente = _clienteServices.FindById(id);
            if (cliente == null) return NotFound();

            return Ok(cliente.Adapt<ClienteDto>());
        }

        [HttpPost]
        public ActionResult<ClienteDto> Create(ClienteDto clienteDto)
        {
            var cliente = clienteDto.Adapt<Cliente>();
            var novoCliente = _clienteServices.Create(cliente);

            return Ok(novoCliente.Adapt<ClienteDto>());
        }

        [HttpPut("{id}")]
        public ActionResult<ClienteDto> Update(int id, ClienteDto clienteDto)
        {
            if (id != clienteDto.ClienteId)
            {
                return BadRequest();
            }

            var cliente = clienteDto.Adapt<Cliente>();
            var clienteAtualizado = _clienteServices.Update(cliente);

            if (clienteAtualizado == null)
            {
                return NotFound();
            }

            return Ok(clienteAtualizado.Adapt<ClienteDto>());
        }
    }
}
