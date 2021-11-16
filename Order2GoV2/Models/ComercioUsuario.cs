using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Order2GoV2.Models
{
    public partial class ComercioUsuario
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DisplayName("Usuario")]
        public int IdUsuario { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DisplayName("Comercio")]
        public int IdComercio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool? Estado { get; set; }

        public virtual Comercio IdComercioNavigation { get; set; }
        public virtual Usuarios IdUsuarioNavigation { get; set; }
    }
}
