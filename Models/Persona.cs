﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CrudMVCApp.Models
{
    public class Persona
    {
        //AÑADIRR VALIDACIONES A LOS CAMPOS, EJ: REQUERIDO, LONGITUD MAXIMA, ETC.
        public int Id { get; set; } // Clave primaria necesaria para EF
        public string Nombre { get; set; }
       
        [Display(Name = "Apellido del Cliente")]
        public string Apellido { get; set; }
        
        public int Dni { get; set; }
        public string Cuit { get; set; }
        public bool Futbol { get; set; }
        public bool Basquet { get; set; }
        public bool Otros { get; set; }
        public char Genero { get; set; }

        //Propiedad navegacion 
        [ValidateNever]
        public ICollection<Direccion> Direcciones { get; set; }
        [ValidateNever]
        public ICollection<Pedido> Pedidos { get; set; } // Relacion uno a muchos con Pedido
        public Persona (){} 
        
    }
}

