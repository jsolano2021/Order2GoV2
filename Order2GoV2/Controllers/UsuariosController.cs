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
    public class UsuariosController : Controller
    {
        private readonly Proyecto1Context _context;

        public UsuariosController(Proyecto1Context context)
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

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            var proyecto1Context = _context.Usuarios.Include(u => u.IdPerfilNavigation);
            return View(await proyecto1Context.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");


            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.IdPerfilNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Nombre");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,Nombre,Apellidos,UserName,Clave,IdPerfil")] Usuarios usuarios)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (ModelState.IsValid)
            {
                _context.Add(usuarios);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Nombre", usuarios.IdPerfil);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Nombre", usuarios.IdPerfil);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,Nombre,Apellidos,UserName,Clave,IdPerfil")] Usuarios usuarios)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id != usuarios.IdUsuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuariosExists(usuarios.IdUsuario))
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
            ViewData["IdPerfil"] = new SelectList(_context.Perfil, "IdPerfil", "Nombre", usuarios.IdPerfil);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            if (id == null)
            {
                return NotFound();
            }

            var usuarios = await _context.Usuarios
                .Include(u => u.IdPerfilNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuarios == null)
            {
                return NotFound();
            }

            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!ValidarUsuarioAdmin())
                return RedirectToAction("Login", "Login");

            var usuarios = await _context.Usuarios.FindAsync(id);
            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.IdUsuario == id);
        }
    }
}
