using Recursos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP.Net.Models
{
    [Table("Mascota")]
    public class Mascota : IValidatableObject
    {
        public int id { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "mascotaType")]
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "msg_required")]
        [StringLength(30, ErrorMessage ="El campo raza tiene una longitud de {2} a {1} caracteres",MinimumLength =3)]
        public string type { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "gender")]
        public bool gender { get; set; }
        [Required(ErrorMessageResourceType = typeof(Recurso), ErrorMessageResourceName = "msg_required")]
        [Range(1,200,ErrorMessage ="El costo tiene un rango de {1} a {2} ")]
        [Display(ResourceType = typeof(Recurso), Name = "cost")]
        public decimal cost { get; set; }
        [Display(ResourceType = typeof(Recurso), Name = "price")]
        public decimal price { get; set; }
        [Display(ResourceType =typeof(Recurso),Name ="description")]
        [StringLength(300,ErrorMessageResourceType =typeof(Recurso),ErrorMessageResourceName ="description_msg",MinimumLength =5)]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }
        public virtual Cliente Cliente { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (price <= cost)
            {
                errors.Add(new ValidationResult("El precio no puede ser menor que el costo", new string[] { "price" }));
            }
            return errors;
        }
    }
}