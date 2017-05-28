using PetShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShopWeb.ViewModels
{
    public class ControlViewModel : RoleModelo
    {
        public ApplicationUser ApplicationUser { get; set; }
    }
}