using BackendNetCore.T24ApiNetCore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        
        public ClientesController(T24Context context) => Context = context;

        T24Context Context { get; set; }

        [HttpGet]
        public async  Task<ActionResult<List<Cliente>>> Get()
        {
            return await Context.Clientes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            ActionResult<Cliente> result;
            Cliente cliente =await Context.FindAsync<Cliente>(id);
            
            if (Equals(cliente, default))
            {
                result = NotFound();
            }
            else
            {
                result = cliente;
            }

            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,Cliente cliente)
        {
            ActionResult result;

            if (id == cliente.Id)
            {
                Context.Entry(cliente).State = EntityState.Modified;
                try
                {
                    await Context.SaveChangesAsync();
                    result = Ok();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (Equals(await Context.FindAsync<Cliente>(id), default))
                    {
                        throw;
                    }
                    else
                    {
                        result = NotFound();
                    }
                }
            }
            else
            {
                result = BadRequest();
            }
            return result;
        }
        [HttpPost]
        public async Task<ActionResult<Cliente>> Post(Cliente cliente)
        {
            Context.Clientes.Add(cliente);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get),new { id = cliente.Id },cliente);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> Delete(int id)
        {
            ActionResult<Cliente> result;
            Cliente cliente = await Context.FindAsync<Cliente>(id);
            if (!Equals(cliente, default))
            {
                Context.Remove(cliente);
                await Context.SaveChangesAsync();
                result = cliente;
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

    }
}
