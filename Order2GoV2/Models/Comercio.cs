using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Order2GoV2.Models
{
    public partial class Comercio
    {
        public Comercio()
        {
            ComercioUsuario = new HashSet<ComercioUsuario>();
            Inventario = new HashSet<Inventario>();
            Venta = new HashSet<Venta>();
        }

        public int IdComercio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int NumeroTelefono { get; set; }

        public virtual ICollection<ComercioUsuario> ComercioUsuario { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<Venta> Venta { get; set; }
    }
}
