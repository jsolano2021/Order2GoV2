using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Order2GoV2.Models;

namespace Order2GoV2.Controllers
{
    public class InventariosController : Controller
    {
        private readonly Proyecto1Context _context;

        public InventariosController(Proyecto1Context context)
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

        // GET: Inventarios
        public async Task<IActionResult> Index()
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            var proyecto1Context = _context.Inventario.Include(i => i.ComercioNavigation).Include(i => i.ProductoNavigation);
            return View(await proyecto1Context.ToListAsync());
        }

        // GET: Inventarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");


            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .Include(i => i.ComercioNavigation)
                .Include(i => i.ProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // GET: Inventarios/Create
        public IActionResult Create()
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login"); 

            var comercios = _context.ComercioUsuario.Where(x => x.IdUsuario == getUser().IdUsuario).Select(x => x.IdComercioNavigation).ToList();



            ViewData["Comercio"] = new SelectList(comercios, "IdComercio", "Descripcion");

            ViewData["Producto"] = new SelectList(_context.Productos, "IdProducto", "Descripcion");
            return View();
        }

        // POST: Inventarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Producto,Comercio,Cantidad,Estado")] Inventario inventario)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                if (!ProductoExists(inventario.Producto))
                {
                    _context.Add(inventario);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["Comercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion", inventario.Comercio);
            ViewData["Producto"] = new SelectList(_context.Productos, "IdProducto", "Descripcion", inventario.Producto);
            return View(inventario);
        }

        // GET: Inventarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");


            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }
            ViewData["Comercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion", inventario.Comercio);
            ViewData["Producto"] = new SelectList(_context.Productos, "IdProducto", "Descripcion", inventario.Producto);
            return View(inventario);
        }

        // POST: Inventarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Producto,Comercio,Cantidad,Estado")] Inventario inventario)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (id != inventario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventarioExists(inventario.Id))
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
            ViewData["Comercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion", inventario.Comercio);
            ViewData["Producto"] = new SelectList(_context.Productos, "IdProducto", "Descripcion", inventario.Producto);
            return View(inventario);
        }

        // GET: Inventarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var inventario = await _context.Inventario
                .Include(i => i.ComercioNavigation)
                .Include(i => i.ProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventario == null)
            {
                return NotFound();
            }

            return View(inventario);
        }

        // POST: Inventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ValidarUsuarioVendedor())
                return RedirectToAction("Login", "Login");

            var inventario = await _context.Inventario.FindAsync(id);
            _context.Inventario.Remove(inventario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventario.Any(e => e.Id == id);
        }


        private bool ProductoExists(int IdProducto)
        {
            return _context.Inventario.Any(e => e.Producto == IdProducto);
        }
    }
}
