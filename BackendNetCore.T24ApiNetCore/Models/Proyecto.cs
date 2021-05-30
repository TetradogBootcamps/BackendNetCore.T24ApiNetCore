using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class Proyecto
    {
        public Proyecto() => AsignadoAs = new List<AsignadoA>();

        [MaxLength(4)]
        public string Id { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }
        public int Horas { get; set; }
        [JsonIgnore]
        public ICollection<AsignadoA> AsignadoAs { get; set; }
    }
}
