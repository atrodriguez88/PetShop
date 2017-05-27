using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Cliente
    {
        public int id { get; set; }
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "msg_required")]
        [Display(ResourceType = typeof(Recurso), Name = "name")]
        [StringLength(30, ErrorMessage = "El campo Nombre tiene una longitud de {2} a {1} caracteres", MinimumLength = 3)]
        public string name { get; set; }
        [EmailAddress(ErrorMessage = "Error al escribir el Email")]
        [Display(ResourceType = typeof(Recurso), Name = "email")]
        public string email { get; set; }
        [NotMapped]
        [Compare("email", ErrorMessage = "No concuerdan los Email")]
        public string confirEmail { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "phone")]
        public int phone { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "gender")]
        [StringLength(5)]
        public string gender { get; set; }

        public virtual List<Mascota> Mascotas { get; set; }
    }
}