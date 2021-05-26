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
    public class ProyectosController : ControllerBase
    {
        public ProyectosController(T24Context context) => Context = context;

        T24Context Context { get; set; }

        [HttpGet]
        public async Task<ActionResult<List<Proyecto>>> Get()
        {
            return await Context.Proyectos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> Get(int id)
        {
            ActionResult<Proyecto> result;
            Proyecto proyectos = await Context.FindAsync<Proyecto>(id);

            if (Equals(proyectos, default))
            {
                result = NotFound();
            }
            else
            {
                result = proyectos;
            }

            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Proyecto proyecto)
        {
            ActionResult result;

            if (id == proyecto.Id)
            {
                Context.Entry(proyecto).State = EntityState.Modified;
                try
                {
                    await Context.SaveChangesAsync();
                    result = Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Equals(await Context.FindAsync<Proyecto>(id), default))
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
        public async Task<ActionResult<Proyecto>> Post(Proyecto proyecto)
        {
            Context.Proyectos.Add(proyecto);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = proyecto.Id }, proyecto);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proyecto>> Delete(int id)
        {
            ActionResult<Proyecto> result;
            Proyecto proyecto = await Context.FindAsync<Proyecto>(id);
            if (!Equals(proyecto, default))
            {
                Context.Remove(proyecto);
                await Context.SaveChangesAsync();
                result = proyecto;
            }
            else
            {
                result = NotFound();
            }
            return result;
        }
    }
}
