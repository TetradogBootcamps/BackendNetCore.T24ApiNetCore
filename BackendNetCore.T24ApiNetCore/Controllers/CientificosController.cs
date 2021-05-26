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

            if (id.ToString() == cientifico.Id)
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

        [HttpPost("{idCientifico}/asignarProyecto/{idProyecto}")]
        public async Task<ActionResult<AsignadoA>> Post(string idCientifico, string idProyecto)
        {
            Proyecto proyecto;
            AsignadoA asignadoA;
            Cientifico cientifico = await Context.Cientificos.FindAsync(idCientifico);
            ActionResult result = NotFound();

            if (!Equals(cientifico, default))
            {
                proyecto = await Context.Proyectos.FindAsync(idProyecto);
                if (!Equals(proyecto, default))
                {
                    asignadoA = new AsignadoA(cientifico,proyecto);

                    if (!await Context.AsignadoAs.AnyAsync(a => a.Equals(asignadoA)))
                    {
                        Context.AsignadoAs.Add(asignadoA);
                        await Context.SaveChangesAsync();

                    }
                    result = Ok(asignadoA);
                }
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
            Cientifico cientifico = await Context.FindAsync<Cientifico>(id);
            if (!Equals(cientifico, default))
            {
                Context.Remove(cientifico);
                await Context.SaveChangesAsync();
                result = cientifico;
            }
            else
            {
                result = NotFound();
            }
            return result;
        }
        [HttpDelete("{idCientifico}/quitarProyecto/{idProyecto}")]
        public async Task<ActionResult<AsignadoA>> Delete(string idCientifico,string idProyecto)
        {
            Proyecto proyecto;
            AsignadoA asignadoA;
            Cientifico cientifico = await Context.Cientificos.FindAsync(idCientifico);
            ActionResult result = NotFound();

            if (!Equals(cientifico, default))
            {
                proyecto = await Context.Proyectos.FindAsync(idProyecto);
                if (!Equals(proyecto, default))
                {
                    asignadoA = new AsignadoA { CientificoId = idCientifico, ProyectoId = idProyecto };
                    asignadoA = await Context.AsignadoAs.FirstOrDefaultAsync(a => a.Equals(asignadoA));

                    if (!Equals(asignadoA, default))
                    {
                        //creo que si se configura bien queda todo bien asignado...
                        asignadoA.Cientifico = cientifico;
                        asignadoA.Proyecto = proyecto;

                        Context.AsignadoAs.Remove(asignadoA);
                        await Context.SaveChangesAsync();
                        result = Ok(asignadoA);
                    }


                }
            }

            return result;
        }
    }
}
