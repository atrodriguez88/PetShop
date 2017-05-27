using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopModel.Type
{
    public class TipoDeMascota
    {
        [Key]
        public int IdTipo { get; set; }
        public string Name { get; set; }
        public virtual List<Mascota> Mascotas { get; set; }
        public virtual List<Alimento> Alimentos { get; set; }
        public virtual List<Utiles> UtilesS { get; set; }
    }
}
