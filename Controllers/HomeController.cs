using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebAppUsuarios.Models;

namespace WebAppUsuarios.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private AccesoDatos _acceso  ; 
        public HomeController(AccesoDatos acceso)
        {
            _acceso = acceso;
        }

        [HttpPost]
        public IActionResult Submit(usuarios modelo)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", modelo);
            }
            try
            {
                _acceso.AgregarUsuario(modelo);

                //si al agregar el usuario es exitoso
                TempData["SuccessMessage"] = "Tu usuario se guardó con éxito.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = "Tu usuario no se guardó."+ex.Message;
                return View("Index", modelo);
            }
        }

        public IActionResult Index()
        {
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
    }
}
