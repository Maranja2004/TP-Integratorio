using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using System.ComponentModel.DataAnnotations;
namespace CrudMVCApp.Models
{
    public class Direccion
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo calle es obligatorio.")]
        [StringLength(150, ErrorMessage = "La calle no puede exceder los 150 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "La calle no puede contener caracteres especiales.")]
        public string Calle { get; set; }

        [Required(ErrorMessage = "El campo ciudad es obligatorio.")]
        [MaxLength(15, ErrorMessage = "La ciudad debe tener maximo 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "La ciudad solo puede contener letras y espacios.")]
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "El campo codigo postal es obligatorio.")]
        public string CodigoPostal { get; set; }
        // Clave Foranea
        public int PersonaId { get; set; } //ESTO ES UNA CLAVE FORANEA, DEBE SER IGUAL A LA CLAVE PRIMARIA DE LA TABLA PERSONA

        // propiedad de navegación
        [ValidateNever] // Evita la validación de este campo en los formularios, permite crear una persona sin que esta este referenciada en una direccion
        public Persona? Persona { get; set; } // Esto permite que una persona tenga una o varias direcciones, y una direccion pertenezca a una persona

        public Direccion() { }
    }
}
