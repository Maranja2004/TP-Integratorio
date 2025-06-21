using CrudMVCApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CrudMVCApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context) // Constructor que recibe el contexto de la base de datos
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public IActionResult Login(string user, string clave)
        {
            var usuario = _context.Usuario
                .FirstOrDefault(u => u.user == user && u.clave == clave); //verificamos que el usuario existe
            if (user == null || clave == null)
            {
                ViewBag.Mensaje = "Usuario o contraseña no pueden estar vacios";
                return View("Index");
            }
            if (usuario != null)
            { //Setemos los datos de la sesion
                HttpContext.Session.SetInt32("Id", usuario.id); //Guardamos el id del usuario en la sesion
                HttpContext.Session.SetString("User", usuario.user);
                HttpContext.Session.SetString("Rol", usuario.rol);
                //lo llevamos a la pag home
                return RedirectToAction("Index", "Home");
            }
            
            ViewBag.Mensaje = "Usuario o contraseña incorrectos";
            return View("Index");
        }
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); //Limpia los datos de la sesion
            return RedirectToAction("Index");
        }
    }
}
