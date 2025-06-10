using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CrudMVCApp.Models;

namespace CrudMVCApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
        {
            return RedirectToAction("Index", "Login");
        }
        return View(); 
    }

    /*public IActionResult Privacy() //en cada accion hay que verificar que la sesion no sea nula, si es nula redirigir a login -> si es null log out y a la cuenta
    {
        if (Session["Usuario"] == null)
        {
            return RedirectToAction("Login", "Cuenta");
        }
        return View();S
    }*/
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
