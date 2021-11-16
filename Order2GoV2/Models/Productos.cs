using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Order2GoV2.Models
{
    public partial class Productos
    {
        public Productos()
        {
            DetalleVenta = new HashSet<DetalleVenta>();
            Inventario = new HashSet<Inventario>();
        }

        public int IdProducto { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public double Precio { get; set; }
      
        public byte[] Imagen { get; set; }

        [NotMapped]
        [Required]
        [DisplayName("Subir Imagen...")]
        public IFormFile ImagenFile { get; set; }

        public virtual ICollection<DetalleVenta> DetalleVenta { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
    }
}
