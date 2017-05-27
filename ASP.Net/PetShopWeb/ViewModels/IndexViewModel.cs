using PetShopModel;
using PetShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShopWeb.ViewModels
{
    public class IndexViewModel : BaseModelo
    {
        public List<Mascota> Mascotas { get; set; }
        public List<Utiles> Utiles { get; set; }
        public List<Alimento> Alimentos { get; set; }
    }
}