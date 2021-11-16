using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Order2GoV2.Models
{
    public class ReporteParameter
    {
        [Required]
        [DisplayName("Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }
        [Required]
        [DisplayName("Fecha de Fin")]
        public DateTime FechaFin { get; set; } 
        public int Comercio { get; set; }
    }
}
