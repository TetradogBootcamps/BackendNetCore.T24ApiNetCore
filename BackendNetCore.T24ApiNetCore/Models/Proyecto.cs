using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class Cientifico
    {
        public int Id => DNI;
        [Key]
        public int DNI { get; set; }
        [MaxLength(255)]
        public string NombreCompleto { get; set; }
        public ICollection<AsignadoA> AsignadoAs { get; set; }
    }
}
