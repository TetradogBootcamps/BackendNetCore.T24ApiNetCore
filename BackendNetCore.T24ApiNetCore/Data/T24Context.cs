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
            modelBuilder.Entity<Video>()
                .HasOne(v => v.Cliente)
                .WithMany(c => c.Videos)
                .HasForeignKey(v => v.ClienteId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<AsignadoA>()
                .HasKey(bc => new { bc.ProyectoId, bc.CientificoId });
            modelBuilder.Entity<AsignadoA>()
                .HasOne(bc => bc.Cientifico)
                .WithMany(b => b.AsignadoAs)
                .HasForeignKey(bc => bc.CientificoId);
            modelBuilder.Entity<AsignadoA>()
                .HasOne(bc => bc.Proyecto)
                .WithMany(c => c.AsignadoAs)
                .HasForeignKey(bc => bc.ProyectoId);

        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Cientifico> Cientificos { get; set; }
        public DbSet<AsignadoA> AsignadoAs { get; set; }
    }
}
