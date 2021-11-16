using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Order2GoV2.Models
{
    public partial class DetalleVenta
    {
        public int Id { get; set; }
        public int Producto { get; set; }
        public int Venta { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }

        public virtual Productos ProductoNavigation { get; set; }
        public virtual Venta VentaNavigation { get; set; }
    }
}
