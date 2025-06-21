﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CrudMVCApp.Models
{
    public class Persona
    {
        public int Id { get; set; } 
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
        public ICollection<Pedido> Pedidos { get; set; } 
        public Persona (){} 
        
    }
}

