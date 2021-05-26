using BackendNetCore.T24ApiNetCore.Data;
using BackendNetCore.T24ApiNetCore.Models;
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
    public class CientificosController : ControllerBase
    {
        public CientificosController(T24Context context) => Context = context;

        T24Context Context { get; set; }

        [HttpGet]
        public async Task<ActionResult<List<Cientifico>>> Get()
        {
            return await Context.Cientificos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cientifico>> Get(int id)
        {
            ActionResult<Cientifico> result;
            Cientifico cientifico = await Context.FindAsync<Cientifico>(id);

            if (Equals(cientifico, default))
            {
                result = NotFound();
            }
            else
            {
                result = cientifico;
            }

            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Cientifico cientifico)
        {
            ActionResult result;

            if (id == cientifico.Id)
            {
                Context.Entry(cientifico).State = EntityState.Modified;
                try
                {
                    await Context.SaveChangesAsync();
                    result = Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Equals(await Context.FindAsync<Cientifico>(id), default))
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
        public async Task<ActionResult<Cientifico>> Post(Cientifico cientifico)
        {
            Context.Cientificos.Add(cientifico);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = cientifico.Id }, cientifico);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cientifico>> Delete(int id)
        {
            ActionResult<Cientifico> result;
            Cientifico cientificos = await Context.FindAsync<Cientifico>(id);
            if (!Equals(cientificos, default))
            {
                Context.Remove(cientificos);
                await Context.SaveChangesAsync();
                result = cientificos;
            }
            else
            {
                result = NotFound();
            }
            return result;
        }
    }
}
