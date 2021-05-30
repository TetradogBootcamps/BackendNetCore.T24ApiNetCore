using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class AsignadoA
    {
        public AsignadoA() { }
        public AsignadoA(Cientifico cientifico,Proyecto proyecto)
        {
            Cientifico = cientifico;
            Proyecto = proyecto;
            CientificoId = cientifico.Id;
            ProyectoId = proyecto.Id;
        }

        public string CientificoId { get; set; }
        [JsonIgnore]
        public Cientifico Cientifico { get; set; }

        public string ProyectoId { get; set; }
        [JsonIgnore]
        public Proyecto Proyecto { get; set; }

    }
}
