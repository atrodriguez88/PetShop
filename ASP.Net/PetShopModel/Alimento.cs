using PetShopModel.Type;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopModel
{
    public class Alimento
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public DateTime BuyTime { get; set; }

        [Required]
        public int IdTipo { get; set; }
        public virtual TipoDeMascota TipoDeMascota { get; set; }
    }
}
