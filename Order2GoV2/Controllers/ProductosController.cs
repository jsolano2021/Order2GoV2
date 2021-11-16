using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Order2GoV2.Models;

namespace Order2GoV2.Controllers
{
    public class ProductosController : Controller
    {
        private readonly Proyecto1Context _context;

        public ProductosController(Proyecto1Context context)
        {
            _context = context;
        }

        bool ValidarUsuarioVendedor()
        {
            string json = HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(json))
            {
                Usuarios miUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuarios>(json);
                if (miUsuario == null || miUsuario.IdPerfil != 2)
                    return false;
            }
            else
                return false;

            return true;
        }


        // GET: Productos
        public async Task<IActionResult> Index()
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            return View(await _context.Productos.ToListAsync());
        }

        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,Nombre,Descripcion,Cantidad,Precio,ImagenFile")] Productos productos)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                if (productos.ImagenFile != null)
                {
                    if (productos.ImagenFile.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            productos.ImagenFile.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            productos.Imagen = fileBytes;
                        }
                    }
                }


                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,Nombre,Descripcion,Cantidad,Precio,ImagenFile")] Productos productos)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (id != productos.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (productos.ImagenFile != null)
                    {
                        if (productos.ImagenFile.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                productos.ImagenFile.CopyTo(ms);
                                var fileBytes = ms.ToArray();
                                productos.Imagen = fileBytes;
                            }
                        }
                    }

                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.IdProducto))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var productos = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            var productos = await _context.Productos.FindAsync(id);
            _context.Productos.Remove(productos);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
