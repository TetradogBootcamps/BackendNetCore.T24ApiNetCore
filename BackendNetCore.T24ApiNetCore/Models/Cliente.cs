using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore
{
    public class Cliente
    {
        //public Cliente()
        //{
        //    Videos = new List<Video>();
        //}
        public int Id { get; set; }
        [MaxLength(250)]
        public string Nombre { get; set; }
        [MaxLength(250)]
        public string Apellido { get; set; }
        [MaxLength(250)]
        public string Direccion { get; set; }
        public int? DNI { get; set; }
        public DateTime? Fecha { get; set; }
        //[NotMapped]
        //public List<Video> Videos { get; set; }
    }
}
