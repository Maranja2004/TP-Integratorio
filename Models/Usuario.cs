using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CrudMVCApp.Models
{ //NO SCAFOLDING SE NECESITA UNA SOLA PAGINA, SOLO NECESITA EL LOGIN Y REGISTRO DE USUARIOS, NO SE NECESITA CRUD PARA USUARIOS
    public class Usuario
    {
        public int id { get; set; }
        [Required]
        public string user { get; set; }
        [Required]
        public string clave { get; set; }
        public string rol { get; set; }

        //PARA ELEGIR ROL UN SELECT

        [ValidateNever] // Evita la validación de este campo en los formularios, permite crear un usuario sin que este referenciado en un pedido
        public ICollection<Pedido> Pedidos { get; set; } // Esto permite que un usuario tenga uno o varios pedidos, y un pedido pertenezca a un usuario
        public Usuario() {
        }
    }
}
