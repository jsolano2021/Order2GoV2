using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Order2GoV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order2GoV2.Controllers
{
    public class ReporteController : Controller
    {

        private readonly Proyecto1Context _context;

        public ReporteController (Proyecto1Context context)
        {
            _context = context;
        }


        Usuarios getUser()
        {
            string json = HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(json))
            {
                Usuarios miUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuarios>(json);
                return miUsuario;
            } 
            return null;
        }


        public IActionResult ReporteVenta()
        {
            var comercios = _context.ComercioUsuario.Where(x => x.IdUsuario == getUser().IdUsuario).Select(x => x.IdComercioNavigation).ToList(); 
            ViewData["Comercio"] = new SelectList(comercios, "IdComercio", "Descripcion");

            return View();
        }

         
        [HttpPost]
        public IActionResult GetReporteVentas([Bind("FechaInicio,FechaFin,Comercio")]  ReporteParameter reporte)
        {

            if (reporte != null 
                && reporte.FechaInicio != null 
                && reporte.FechaFin != null 
                && reporte.Comercio > 0)
            { 

                if(reporte.FechaFin >= reporte.FechaInicio)
                {
                    var test = _context.DetalleVenta.ToList();

                    var query2 = (
                                    from d in _context.DetalleVenta
                                    from v in _context.Venta
                                    from p in _context.Productos
                                    where v.Codigo == d.Venta && p.IdProducto == d.Producto
                                    && v.Comercio == reporte.Comercio
                                    select new DetalleVenta
                                    { 
                                        Id = d.Id, 
                                        Cantidad = d.Cantidad, 
                                        Subtotal = d.Subtotal, 
                                        Venta = d.Venta,
                                        Producto = d.Producto,
                                        VentaNavigation = v,
                                        ProductoNavigation = p
                                    }
                        ).ToList(); 


                    ViewData["ListaDatos"] = query2;
                    ViewData["Total"] = query2.Sum(x => x.Subtotal);

                    var comercios = _context.ComercioUsuario.Where(x => x.IdUsuario == getUser().IdUsuario).Select(x => x.IdComercioNavigation).ToList();
                    ViewData["Comercio"] = new SelectList(comercios, "IdComercio", "Descripcion");
                    return View("ReporteVenta");
                }
            }

            return View(null);
        }
    }
}
