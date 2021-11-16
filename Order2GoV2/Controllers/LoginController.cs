
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Order2GoV2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order2GoV2.Controllers
{
    public class LoginController : Controller
    {
        private readonly Proyecto1Context _context;

        public LoginController(Proyecto1Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Usuarios user)
        {

            if (user != null)
            {
                using (var context = new Proyecto1Context())
                {
                    Usuarios miUsuario = context.Usuarios.Where(x => x.UserName.ToUpper().Equals(user.UserName.ToUpper())
                            && x.Clave.Equals(user.Clave)).FirstOrDefault();

                    if (miUsuario != null)
                    {
                        HttpContext.Session.SetString("Usuario", JsonConvert.SerializeObject(miUsuario));

                        if (miUsuario.IdPerfil == 1)
                        {
                            return RedirectToAction("Index", "HomeAdmin");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return RedirectToAction("Login", "Login");
                    }
                }
            }

            return View();
        }
    }
}
