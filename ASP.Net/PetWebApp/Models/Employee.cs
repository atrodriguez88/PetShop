using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Recursos;

namespace ASP.Net.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int id { get; set; }
        [Display(ResourceType =typeof(Recurso),Name ="name")]
        [Required(ErrorMessageResourceType =typeof(Recurso),ErrorMessageResourceName = "msg_required")]
        [StringLength(30,ErrorMessage ="El campo Nombre tiene una longitud de {2} a {1} caracteres",MinimumLength =3)]
        public string name { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "edad")]
        [Range(15,80,ErrorMessage ="La edad tiene un rango de {1} a {2} años")]
        public int edad { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "email")]
        [EmailAddress(ErrorMessage ="Error al escribir el Email")]
        public string email { get; set; }
        [Phone(ErrorMessage ="Error al escribir el telf")]
        [Display(ResourceType = typeof(Recurso), Name = "phone")]
        public int phone { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "creditCard")]
        [CreditCard]
        public string creditCard { get; set; }
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "msg_required")]
        [Display(ResourceType = typeof(Recurso), Name = "salary")]
        public decimal salary { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "gender")]
        public bool gender { get; set; }
    }
}