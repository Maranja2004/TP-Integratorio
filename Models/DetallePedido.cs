using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CrudMVCApp.Models
{
    public class DetallePedido
    {
        public int id { get; set; }
        public int PedidoId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }

        public double PrecioUnitario { get; set; }
        public double Subtotal => Cantidad * PrecioUnitario; // Calcula el subtotal multiplicando la cantidad por el precio unitario
        [ValidateNever]
        public Producto? Producto { get; set; } // Propiedad de navegación para el producto asociado a este detalle
        [ValidateNever]
        public Pedido? Pedido { get; set; } // Propiedad de navegación para el pedido al que pertenece este detalle

        public DetallePedido() { } // Constructor por defecto
    }
}
