using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroCompleto.Models;
using CadastroCompleto.Service;
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
        public ActionResult<List<Cliente>> GetAll()
        {
            var clientes = _clienteServices.FindAll();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetById(int id)
        {
            var cliente = _clienteServices.FindById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> Create(Cliente cliente)
        {
            var novoCliente = _clienteServices.Create(cliente);
            return CreatedAtAction(nameof(GetById), new { id = novoCliente.ClienteId }, novoCliente);
        }

        [HttpPut("{id}")]
        public ActionResult<Cliente> Update(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            var clienteAtualizado = _clienteServices.Update(cliente);
            if (clienteAtualizado == null)
            {
                return NotFound();
            }

            return Ok(clienteAtualizado);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _clienteServices.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
