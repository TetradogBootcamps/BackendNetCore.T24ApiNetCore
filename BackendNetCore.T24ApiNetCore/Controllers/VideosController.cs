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
    public class VideosController : ControllerBase
    {
        public VideosController(T24Context context) => Context = context;

        T24Context Context { get; set; }

        [HttpGet]
        public async Task<ActionResult<List<Video>>> Get()
        {
            return await Context.Videos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> Get(int id)
        {
            ActionResult<Video> result;
            Video video = await Context.FindAsync<Video>(id);

            if (Equals(video, default))
            {
                result = NotFound();
            }
            else
            {
                result = video;
            }

            return result;
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Video video)
        {
            ActionResult result;

            if (id == video.Id)
            {
                Context.Entry(video).State = EntityState.Modified;
                try
                {
                    await Context.SaveChangesAsync();
                    result = Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (Equals(await Context.FindAsync<Video>(id), default))
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
        public async Task<ActionResult<Video>> Post(Video video)
        {
            if (video.ClienteId.HasValue && !(await Context.Clientes.AnyAsync(c=>c.Id==video.ClienteId.Value)))
            {
                Log.WriteLines($"post video con clientId '{video.ClienteId}' no existente!");
                video.ClienteId = default;//si no lo encuentra lo quito para que no de problemas
            }
            Context.Videos.Add(video);
            await Context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = video.Id }, video);



        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Video>> Delete(int id)
        {
            ActionResult<Video> result;
            Video video = await Context.FindAsync<Video>(id);
            if (!Equals(video, default))
            {
                Context.Remove(video);
                await Context.SaveChangesAsync();
                result = video;
            }
            else
            {
                result = NotFound();
            }
            return result;
        }
    }
}
