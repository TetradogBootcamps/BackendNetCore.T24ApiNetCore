﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackendNetCore.T24ApiNetCore.Models
{
    public class Proyecto
    {
    
        public int Id { get; set; }
        [MaxLength(255)]
        public string Nombre { get; set; }
        public int Horas { get; set; }
        public ICollection<AsignadoA> AsignadoAs { get; set; }
    }
}