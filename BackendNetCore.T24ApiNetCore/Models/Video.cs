using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore
{
    public class Video
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Director { get; set; }

        //si el campo es opcional tiene que ser nullable si es struct

        public int? ClienteId { get; set; }
        [JsonIgnore]
        public Cliente Cliente { get; set; }
    }
}
