using PetShopModel.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopModel
{
    public class Mascota : IValidatableObject
    {
        public int Id { get; set; }
        [Display(Name ="Imagen")]
        public string Img { get; set; }
        public bool Gender { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime BuyTime { get; set; }

        [Required]
        public int IdTipo { get; set; }
        public virtual TipoDeMascota TipoDeMascota { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (Price <= Cost)
            {
                errors.Add(new ValidationResult("The price don't can be more higherly than cost", new string[] { "price" }));
            }
            return errors;
        }
    }
}
