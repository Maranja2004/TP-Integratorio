using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CrudMVCApp.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la persona es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido del Cliente")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]*$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El DNI es obligatorio.")]
        public int Dni { get; set; }
        [Required(ErrorMessage = "El CUIT es obligatorio.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El CUIT solo puede contener números.")]
        public string Cuit { get; set; }

        public bool Futbol { get; set; }
        public bool Basquet { get; set; }
        public bool Otros { get; set; }
        [Required(ErrorMessage = "El genero es obligatorio.")]
        [RegularExpression("^[MFmf]$", ErrorMessage = "El género debe ser 'M' o 'F'.")] 
        public char Genero { get; set; }

        //Propiedad navegacion 
        [ValidateNever]
        public ICollection<Direccion> Direcciones { get; set; }
        [ValidateNever]
        public ICollection<Pedido> Pedidos { get; set; } 
        public Persona (){} 
        
    }
}

