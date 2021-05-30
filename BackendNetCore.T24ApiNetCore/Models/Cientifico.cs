using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class Cientifico
    {
        //public Cientifico() => AsignadoAs = new List<AsignadoA>();
        public string Id => DNI;
        [Key,MaxLength(8)]
        public string DNI { get; set; }
        [MaxLength(255)]
        public string NombreCompleto { get; set; }
        //public List<AsignadoA> AsignadoAs { get; set; }
    }
}
