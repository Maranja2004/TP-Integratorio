using Microsoft.AspNetCore.Mvc.ModelBinding.Validation; // This attribute is used to skip validation for this property in forms
using System.ComponentModel.DataAnnotations; // This namespace contains data annotation attributes for validation

namespace CrudMVCApp.Models
{
    public class Producto
    {
        public int id { get; set; }
          [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción del producto es obligatorio.")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres.")]
        public string Descripcion { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "El stock solo puede contener números.")]
        [Required(ErrorMessage = "El precio de compra es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de compra debe ser mayor que cero.")]
        public double PrecioCompra { get; set; }

        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio de venta debe ser mayor que cero.")]
        public double PrecioVenta { get; set; }
        [RegularExpression(@"^\d+$", ErrorMessage = "El stock solo puede contener números.")]
        [Required(ErrorMessage = "El stock del producto es obligatorio.")]
        public int Stock { get; set; }
        [ValidateNever]
        public ICollection<DetallePedido> DetallePedidos { get; set; } //Esto permite que un producto tenga uno o varios detalles de pedido, y un detalle de pedido pertenezca a un producto
        public Producto() { }
    }
 
}
