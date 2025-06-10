using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CrudMVCApp.Models
{
    public class Pedido
    {
        public int Id { get; set; } // Clave primaria necesaria para EF
        public int UsuarioId { get; set; } // Clave foranea del usuario que realiza el pedido
        public int PersonaId { get; set; } // Clave foranea de la persona que realiza el pedido
        public double Total { get; set; } // Total del pedido, se calcula al finalizar el pedido
        public int cantidadProductos { get; set; } // Cantidad de productos en el pedido


        [ValidateNever]
        public Usuario? Usuario { get; set; } // Propiedad de navegación para el usuario que realiza el pedido
        [ValidateNever]
        public Persona? Persona { get; set; } // Propiedad de navegación para la persona que realiza el pedido
        [ValidateNever]
        public ICollection<DetallePedido> DetallePedidos { get; set; } // Colección de detalles del pedido, cada detalle representa un producto en el pedido

        public Pedido() { } // Constructor por defecto
    }
}
