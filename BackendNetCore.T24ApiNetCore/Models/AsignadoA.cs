using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class AsignadoA
    {
        public int CientificoId { get; set; }
        [ForeignKey("CientificoId")]
        public Cientifico Cientifico { get; set; }
        public int ProyectoId { get; set; }
        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; }
    }
}
