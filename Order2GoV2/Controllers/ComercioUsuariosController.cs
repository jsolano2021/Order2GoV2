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
    public class ComercioUsuariosController : Controller
    {
        private readonly Proyecto1Context _context;

        public ComercioUsuariosController(Proyecto1Context context)
        {
            _context = context;
        }

        bool ValidarUsuarioAdmin()
        {
            string json = HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(json))
            {
                Usuarios miUsuario = Newtonsoft.Json.JsonConvert.DeserializeObject<Usuarios>(json);
                if (miUsuario == null || miUsuario.IdPerfil != 1)
                    return false;
            }
            else
                return false;

            return true;
        }

        // GET: ComercioUsuarios
        public async Task<IActionResult> Index()
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            var proyecto1Context = _context.ComercioUsuario.Include(c => c.IdComercioNavigation).Include(c => c.IdUsuarioNavigation);
            return View(await proyecto1Context.ToListAsync());
        }

        // GET: ComercioUsuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var comercioUsuario = await _context.ComercioUsuario
                .Include(c => c.IdComercioNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comercioUsuario == null)
            {
                return NotFound();
            }

            return View(comercioUsuario);
        }

        // GET: ComercioUsuarios/Create
        public IActionResult Create()
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            ViewData["IdComercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(x => x.IdPerfil == 2), "IdUsuario", "Apellidos");
            return View();
        }

        // POST: ComercioUsuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdComercio,FechaRegistro,Estado")] ComercioUsuario comercioUsuario)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                _context.Add(comercioUsuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdComercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion", comercioUsuario.IdComercio);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(x => x.IdPerfil == 2), "IdUsuario", "Apellidos");
            return View(comercioUsuario);
        }

        // GET: ComercioUsuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var comercioUsuario = await _context.ComercioUsuario.FindAsync(id);
            if (comercioUsuario == null)
            {
                return NotFound();
            }
            ViewData["IdComercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion", comercioUsuario.IdComercio);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(x => x.IdPerfil == 2), "IdUsuario", "Apellidos");
            return View(comercioUsuario);
        }

        // POST: ComercioUsuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdComercio,FechaRegistro,Estado")] ComercioUsuario comercioUsuario)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id != comercioUsuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comercioUsuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComercioUsuarioExists(comercioUsuario.Id))
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
            ViewData["IdComercio"] = new SelectList(_context.Comercio, "IdComercio", "Descripcion", comercioUsuario.IdComercio);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios.Where(x => x.IdPerfil == 2), "IdUsuario", "Apellidos");
            return View(comercioUsuario);
        }

        // GET: ComercioUsuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var comercioUsuario = await _context.ComercioUsuario
                .Include(c => c.IdComercioNavigation)
                .Include(c => c.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comercioUsuario == null)
            {
                return NotFound();
            }

            return View(comercioUsuario);
        }

        // POST: ComercioUsuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            var comercioUsuario = await _context.ComercioUsuario.FindAsync(id);
            _context.ComercioUsuario.Remove(comercioUsuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComercioUsuarioExists(int id)
        {
            return _context.ComercioUsuario.Any(e => e.Id == id);
        }
    }
}
