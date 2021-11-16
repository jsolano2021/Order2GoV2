using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Order2GoV2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Order2GoV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

     

        public IActionResult Index()
        {
            string json = HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(json))
            {
                Usuarios miUsuario = JsonConvert.DeserializeObject<Usuarios>(json);

                if (miUsuario == null || miUsuario.IdPerfil != 2)
                {
                    return RedirectToAction("Login", "Login");
                }

                ViewData["Usuario"] = miUsuario.Nombre + " " + miUsuario.Apellidos;

            }
            else
            {
                return RedirectToAction("Login", "Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Usuario");
            return RedirectToAction("Login", "Login");
        }

    }
}
