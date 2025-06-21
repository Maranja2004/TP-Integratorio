using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudMVCApp.Data;
using CrudMVCApp.Models;
using System.Text.Json;

namespace CrudMVCApp.Controllers
{
    public class PedidoController : Controller
    {
        private readonly AppDbContext _context;

        public PedidoController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string buscar)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Index", "Login");
            }
            var pedido = from Pedido in _context.Pedido
                         select Pedido;
            if (!String.IsNullOrEmpty(buscar))
            {
                pedido = pedido.Where(p => p.UsuarioId.ToString().Contains(buscar) || p.Usuario.user.Contains(buscar));
            }

           /* pedido = await _context.Pedido
                .Include(p => p.Persona)
                .Include(p => p.Usuario)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(dp => dp.Producto)
                .ToListAsync();*/ 
            return View(await pedido.Include(p => p.Persona)
                .Include(p => p.Usuario)
                .Include(p => p.DetallePedidos)
                    .ThenInclude(dp => dp.Producto).ToListAsync());
        }
        public IActionResult CreatePedido()
        {
            ViewBag.Personas = new SelectList(_context.Persona, "Id", "Nombre"); 
            return View();
        }
        [HttpPost]
        public IActionResult GuardarPedido() 
        {
            var pedidoJson = HttpContext.Session.GetString("PedidoTemp");
            if (pedidoJson == null || !pedidoJson.Any())
            {
               return RedirectToAction("AgregarDetalle", "DetallePedido");
            }
            var pedido = JsonSerializer.Deserialize<Pedido>(pedidoJson);
            
            pedido.Total = pedido.DetallePedidos.Sum(d => d.Cantidad * d.PrecioUnitario);
            pedido.cantidadProductos = pedido.DetallePedidos.Sum(d => d.Cantidad);
            int? usuarioId = HttpContext.Session.GetInt32("Id");
            if (usuarioId == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            pedido.UsuarioId = usuarioId.Value;

            _context.Pedido.Add(pedido);
            _context.SaveChanges(); // Guardar el pedido en la base de datos

            HttpContext.Session.Remove("PedidoTemp");
            return RedirectToAction("Index");
        }

    }
}
