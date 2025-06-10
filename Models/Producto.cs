using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // This attribute is used to skip validation for this property in forms
using System.ComponentModel.DataAnnotations; // This namespace contains data annotation attributes for validation

namespace CrudMVCApp.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; } 
        public double PrecioCompra { get; set; }
        public double PrecioVenta { get; set; }

        public int Stock { get; set; }
        [ValidateNever]
        public ICollection<DetallePedido> DetallePedidos { get; set; } //Esto permite que un producto tenga uno o varios detalles de pedido, y un detalle de pedido pertenezca a un producto
        public Producto() { }
    }
 
}
