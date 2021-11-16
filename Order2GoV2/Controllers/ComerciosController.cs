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
    public class ComerciosController : Controller
    {
        private readonly Proyecto1Context _context;

        public ComerciosController(Proyecto1Context context)
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

        // GET: Comercios
        public async Task<IActionResult> Index()
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            return View(await _context.Comercio.ToListAsync());
        }

        // GET: Comercios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var comercio = await _context.Comercio
                .FirstOrDefaultAsync(m => m.IdComercio == id);
            if (comercio == null)
            {
                return NotFound();
            }

            return View(comercio);
        }

        // GET: Comercios/Create
        public IActionResult Create()
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            return View();
        }

        // POST: Comercios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComercio,Nombre,Descripcion,NumeroTelefono")] Comercio comercio)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                _context.Add(comercio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comercio);
        }

        // GET: Comercios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var comercio = await _context.Comercio.FindAsync(id);
            if (comercio == null)
            {
                return NotFound();
            }
            return View(comercio);
        }

        // POST: Comercios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComercio,Nombre,Descripcion,NumeroTelefono")] Comercio comercio)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id != comercio.IdComercio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comercio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComercioExists(comercio.IdComercio))
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
            return View(comercio);
        }

        // GET: Comercios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var comercio = await _context.Comercio
                .FirstOrDefaultAsync(m => m.IdComercio == id);
            if (comercio == null)
            {
                return NotFound();
            }

            return View(comercio);
        }

        // POST: Comercios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            var comercio = await _context.Comercio.FindAsync(id);
            _context.Comercio.Remove(comercio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComercioExists(int id)
        {
            return _context.Comercio.Any(e => e.IdComercio == id);
        }
    }
}
