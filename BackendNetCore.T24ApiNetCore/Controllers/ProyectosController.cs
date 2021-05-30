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
        public  async Task<ActionResult<Proyecto>> Get(string id)
        {
            ActionResult<Proyecto> result;
            Proyecto proyecto = await Context.FindAsync<Proyecto>(id);

            if (Equals(proyecto, default))
            {
                result = NotFound();
            }
            else
            {
                result = proyecto;
            }

            return result;
        }
        [HttpGet("{id}/cientificos")]
        public async Task<ActionResult<AsignadoA>> GetCientificos(string id)
        {
            ActionResult<AsignadoA> result;
            List<AsignadoA> cientificosAsignados;
            Proyecto proyecto = await Context.FindAsync<Proyecto>(id);

            if (Equals(proyecto, default))
            {
                result = NotFound();
            }
            else
            {
                cientificosAsignados = new List<AsignadoA>();
                cientificosAsignados.AddRange(Context.AsignadoAs.Where(a => Equals(a.ProyectoId, proyecto.Id)));
                result = Ok(cientificosAsignados);
            }

            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, Proyecto proyecto)
        {
            ActionResult result;

            if (Equals(id,proyecto.Id))
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
        [HttpPost("{idProyecto}/Cientifico/{idCientifico}")]
        public async Task<ActionResult<AsignadoA>> Post(string idProyecto,string idCientifico)
        {
            Proyecto proyecto;
            AsignadoA asignadoA;
            Cientifico cientifico = await Context.FindAsync<Cientifico>(idCientifico);
            ActionResult result = NotFound();

            if (!Equals(cientifico, default))
            {
                proyecto = await Context.FindAsync<Proyecto>(idProyecto);
                if (!Equals(proyecto, default))
                {
                    asignadoA = new AsignadoA(cientifico, proyecto);

                    if (!await Context.ExistAsignadoA(idProyecto,idCientifico))
                    {
                        Context.AsignadoAs.Add(asignadoA);
                        await Context.SaveChangesAsync();

                    }
                    result = Ok(asignadoA);
                }
            }

            return result;
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proyecto>> Delete(string id)
        {
            ActionResult<Proyecto> result;
            Proyecto proyecto =  await Context.FindAsync<Proyecto>(id);
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
        [HttpDelete("{idProyecto}/Cientifico/{idCientifico}")]
        public async Task<ActionResult<AsignadoA>> Delete(string idProyecto, string idCientifico)
        {
            Proyecto proyecto;
            AsignadoA asignadoA;
            Cientifico cientifico = await Context.FindAsync<Cientifico>(idCientifico);
            ActionResult result = NotFound();

            if (!Equals(cientifico, default))
            {
                proyecto = await Context.FindAsync<Proyecto>(idProyecto);
                if (!Equals(proyecto, default))
                {
                    asignadoA =await Context.FindAsignadoA(idProyecto,idCientifico);
            

                    if (!Equals(asignadoA,default))
                    {
                        //asignadoA.Cientifico = cientifico;
                        //asignadoA.Proyecto = proyecto;
                        //no se si es buena idea incluir en el JSON información que si se quiere se puede solicitar...
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
