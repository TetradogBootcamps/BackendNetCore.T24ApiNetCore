using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class AsignadoA
    {
        public AsignadoA() { }
        public AsignadoA(Cientifico cientifico,Proyecto proyecto)
        {
            //Cientifico = cientifico;
            //Proyecto = proyecto;
            CientificoId = cientifico.Id;
            ProyectoId = proyecto.Id;
        }

        public string CientificoId { get; set; }
        //[ForeignKey("CientificoId"),NotMapped]
        //public Cientifico Cientifico { get; set; }

        public string ProyectoId { get; set; }
        //[ForeignKey("ProyectoId"),NotMapped]
        //public Proyecto Proyecto { get; set; }

        public override bool Equals(object obj)
        {
            AsignadoA other = obj as AsignadoA;
            bool equals=!Equals(other,default);
            if (equals)
                equals = Equals(other.CientificoId,CientificoId) && Equals(ProyectoId, other.ProyectoId);
            return equals;
        }
    }
}
