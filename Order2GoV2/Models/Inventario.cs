using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Order2GoV2.Models
{
    public partial class Inventario
    {
        public int Id { get; set; }
        public int Producto { get; set; }
        public int Comercio { get; set; }
        public int Cantidad { get; set; }
        public bool? Estado { get; set; }

        public virtual Comercio ComercioNavigation { get; set; }
        public virtual Productos ProductoNavigation { get; set; }
    }
}
