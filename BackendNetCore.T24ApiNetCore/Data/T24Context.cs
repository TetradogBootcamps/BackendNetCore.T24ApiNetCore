using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackendNetCore.T24ApiNetCore;
using BackendNetCore.T24ApiNetCore.Models;

namespace BackendNetCore.T24ApiNetCore.Data
{
    public class T24Context : DbContext
    {
        public T24Context(DbContextOptions<T24Context> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //    modelBuilder.Entity<Video>()
            //        .HasOne(v => v.Cliente)
            //        .WithMany(c => c.Videos)
            //        .HasForeignKey(v => v.ClienteId)
            //        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AsignadoA>()
                .HasKey(bc => new { bc.ProyectoId, bc.CientificoId });
            //    modelBuilder.Entity<AsignadoA>()
            //        .HasOne(bc => bc.Cientifico)
            //        .WithMany(b => b.AsignadoAs)
            //        .HasForeignKey(bc => bc.CientificoId);
            //    modelBuilder.Entity<AsignadoA>()
            //        .HasOne(bc => bc.Proyecto)
            //        .WithMany(c => c.AsignadoAs)
            //        .HasForeignKey(bc => bc.ProyectoId);


        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Cientifico> Cientificos { get; set; }
        public DbSet<AsignadoA> AsignadoAs { get; set; }

        public async Task<AsignadoA> FindAsignadoA(string idProyecto,string idCientifico)
        {
            return await FindAsync<AsignadoA>(idProyecto, idCientifico);
        }
        public async Task<bool> ExistAsignadoA(string idProyecto, string idCientifico) => !Equals(await FindAsignadoA(idProyecto, idCientifico), default);

        //public Cliente GetCliente(int id)
        //{
        //    Cliente cliente = Find<Cliente>(id);
        //    if (!Equals(cliente, default))
        //    {
        //        cliente.Videos.Clear();
        //        cliente.Videos.AddRange(Videos.Where(v => v.ClienteId == cliente.Id));

        //    }
        //    return cliente;
        //}
        //public Video GetVideo(int id)
        //{
        //    Video video = Find<Video>(id);
        //    if (!Equals(video, default))
        //    {
        //        if (video.ClienteId.HasValue)
        //            video.Cliente = this.GetCliente(video.ClienteId.Value);
        //    }
        //    return video;
        //}
        //public Proyecto GetProyecto(string id)
        //{
        //    AsignadoA[] asignados;
        //    Proyecto proyecto = Find<Proyecto>(id);
        
        //    Log.WriteLines($"Proyecto {id}");
        //    if (!Equals(proyecto, default))
        //    {
        //        proyecto.AsignadoAs.Clear();
        //        asignados = AsignadoAs.Where(a => a.ProyectoId == proyecto.Id).ToArray();

        //        if (asignados.Length > 0)
        //            proyecto.AsignadoAs.AddRange(asignados.Select(a => this.GetAsignadoA(a.CientificoId, a.ProyectoId)));
        //        Log.WriteLines($"Fin asignado {id}-{asignados.Length}");
        //    }
        //    return proyecto;
        //}
        //public Cientifico GetCientifico(string id)
        //{
        //    Cientifico cientifico = Find<Cientifico>(id);
        //    if (!Equals(cientifico, default))
        //    {
        //        cientifico.AsignadoAs.Clear();
        //        cientifico.AsignadoAs.AddRange(AsignadoAs.Where(a => a.CientificoId == cientifico.Id).Select(a => this.GetAsignadoA(a.CientificoId, a.ProyectoId)));
        //    }
        //    return cientifico;
        //}
        //public AsignadoA GetAsignadoA(string idCientifico, string idProyecto)
        //{
        //    AsignadoA asignado = new AsignadoA() { CientificoId = idCientifico, ProyectoId = idProyecto };
        //    asignado.Cientifico = Find<Cientifico>(idCientifico);
        //    asignado.Proyecto = Find<Proyecto>(idProyecto);
        //    Log.WriteLines($"Asigando A {idCientifico} - {idProyecto}");

        //    return asignado;

        //}
    }
}
