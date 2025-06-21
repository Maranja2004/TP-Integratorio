using CrudMVCApp.Data;
using CrudMVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CrudMVCApp.Controllers
{
    public class DetallePedidoController : Controller
    {
        private readonly AppDbContext _context;

        public DetallePedidoController(AppDbContext context)
        {
            _context = context;
        }

        [JsonIgnore]
        public Producto? Producto { get; set; }

        // GET: Vista para agregar detalle
        public IActionResult AgregarDetalle(int? personaId)
        {
            var pedidoJson = HttpContext.Session.GetString("PedidoTemp");

            var pedido = string.IsNullOrEmpty(pedidoJson)
                ? new Pedido { DetallePedidos = new List<DetallePedido>() }
                : JsonSerializer.Deserialize<Pedido>(pedidoJson);

            if (pedido.UsuarioId > 0)
            {
                pedido.Usuario = _context.Usuario.Find(pedido.UsuarioId);
            }
            else
            {
                var usuarioId = HttpContext.Session.GetInt32("Id");
                if (usuarioId.HasValue)
                {
                    pedido.UsuarioId = usuarioId.Value;
                    pedido.Usuario = _context.Usuario.Find(usuarioId.Value);
                }
            }

            foreach (var item in pedido.DetallePedidos) //
            {
                if (item.Producto == null)
                {
                    item.Producto = _context.Producto.Find(item.ProductoId);
                }
            }

            ViewBag.Personas = _context.Persona
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Nombre
                }).ToList();

            ViewBag.Productos = _context.Producto
                .Select(p => new SelectListItem
                {
                    Value = p.id.ToString(),
                    Text = p.id.ToString()
                }).ToList();

            ViewBag.PedidoActual = pedido;
            ViewBag.PersonaId = personaId ?? pedido.PersonaId;

            if (ViewBag.PersonaId != null)
            {
                var persona = _context.Persona.Find(ViewBag.PersonaId);
                if (persona != null)
                {
                    ViewBag.Genero = persona.Genero;
                    ViewBag.Documento = persona.Dni;
                }
            }

            return View(new DetallePedido()); 
        }

        
        [HttpPost]
        public IActionResult AgregarDetalle(DetallePedido detalle, int personaId)
        {
            var pedidoJson = HttpContext.Session.GetString("PedidoTemp");

            var pedido = string.IsNullOrEmpty(pedidoJson)
                ? new Pedido { DetallePedidos = new List<DetallePedido>() }
                : JsonSerializer.Deserialize<Pedido>(pedidoJson);

            var producto = _context.Producto.Find(detalle.ProductoId);
            if (producto != null)
            {
                detalle.PrecioUnitario = producto.PrecioVenta;
            }

            var existente = pedido.DetallePedidos
                .FirstOrDefault(d => d.ProductoId == detalle.ProductoId);

            if (existente != null)
            {
                existente.Cantidad += detalle.Cantidad;
            }
            else
            {
                pedido.DetallePedidos.Add(detalle);
            }

            pedido.PersonaId = personaId;

            HttpContext.Session.SetString("PedidoTemp", JsonSerializer.Serialize(pedido));

            return RedirectToAction("AgregarDetalle", new { personaId = personaId });
        }

        public IActionResult EliminarProducto(int id)
        {
            var pedidoJson = HttpContext.Session.GetString("PedidoTemp");
            if (string.IsNullOrEmpty(pedidoJson))
            {
                return RedirectToAction("AgregarDetalle");
            }
            var pedido = JsonSerializer.Deserialize<Pedido>(pedidoJson);
            var detalle = pedido.DetallePedidos.FirstOrDefault(d => d.id == id);
            if (detalle != null)
            {
                pedido.DetallePedidos.Remove(detalle);
                HttpContext.Session.SetString("PedidoTemp", JsonSerializer.Serialize(pedido));
            }
            return RedirectToAction("AgregarDetalle", new { personaId = pedido.PersonaId });
        }
        public IActionResult EliminarDetalle()
        {
            var pedidoJson = HttpContext.Session.GetString("PedidoTemp");
            if (string.IsNullOrEmpty(pedidoJson))
            {
                return RedirectToAction("AgregarDetalle");
            }
            var pedido = JsonSerializer.Deserialize<Pedido>(pedidoJson);
            pedido.DetallePedidos.Clear(); // Esto elimina todos los detalles
            HttpContext.Session.SetString("PedidoTemp", JsonSerializer.Serialize(pedido));
            return RedirectToAction("AgregarDetalle", new { personaId = pedido.PersonaId });
        }
        // GET: Buscar productos por código (para autocompletar)
        public IActionResult BuscarPorCodigo(string codigo)
        {
            var productos = _context.Producto
                .Where(p => p.id.ToString().Contains(codigo)) // Asegurate que Producto tenga campo Codigo
                .Select(p => new
                {
                    id = p.id,
                    nombre = p.Nombre
                })
                .Take(5)
                .ToList();

            return Json(productos);
        }

        public IActionResult VerDetalle(int id)
        {
            var pedido = _context.Pedido
                .Include(p => p.Persona)
                .Include(p => p.Usuario)
                .FirstOrDefault(p => p.Id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            var detalles = _context.DetallePedido
                .Where(d => d.PedidoId == id)
                .Include(d => d.Producto)
                .ToList();

            ViewBag.IdPedido = pedido.Id;
            ViewBag.ClienteId = pedido.Persona?.Id;
            ViewBag.UsuarioId = pedido.Usuario?.id;
            ViewBag.Total = pedido.Total;
            ViewBag.CantidadProductos = pedido.cantidadProductos;

            return View(detalles);
        }


    }
}
