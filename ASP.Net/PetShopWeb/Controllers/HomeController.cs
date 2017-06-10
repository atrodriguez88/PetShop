using PetShopModel;
using PetShopWeb.Models;
using PetShopWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace PetShopWeb.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {
        [Route("~/")]
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {            
            ViewBag.Title = "PetShop";            
            return View();

            #region Consumiendo Store Procedure
            //Solution 1
            //var namesMale = db.Database.SqlQuery<string>("sp_name @gender", new SqlParameter("@gender", "male")).ToList();
            //Solution 2
            //var namesM = db.Database.ExecuteSqlCommand("sp_name @gender", new SqlParameter("@gender", "male")); 
            #endregion

            #region SQL Inyeccion
            //using (ApplicationDbContext cont = new ApplicationDbContext())
            //{
            //    //Problema
            //    List<Cliente> clientes = new List<Cliente>();
            //    int id = 4;
            //    string query = @"SELECT * FROM Clientes WHERE Id ='" + id + "'";
            //    clientes = cont.Database.SqlQuery<Cliente>(query).ToList();
            //}
            //using (ApplicationDbContext cont = new ApplicationDbContext())
            //{
            //    //Solucion 1 Parametrizar
            //    List<Cliente> clientes = new List<Cliente>();
            //    int id = 4;
            //    string query = @"SELECT * FROM Clientes WHERE Id = @id";
            //    clientes = cont.Database.SqlQuery<Cliente>(query, new SqlParameter("@id", id)).ToList();
            //    //Solucion 2 Utilizando Entity Framework y lambda
            //    List<Cliente> clientes1 = new List<Cliente>();
            //    int id1 = 4;
            //    clientes = cont.Clientes.Where(c => c.id == id1).ToList();
            //} 
            #endregion
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Contact()
        {
            Persona persona = new Persona()
            {
                Name = "Ariel",
                Nacimento = DateTime.Now
            };
            ViewBag.Persona = persona;
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [Route("Warning")]
        [ChildActionOnly]
        public ActionResult Warning()
        {
            ViewBag.Message = "Your warning page.";
            return View();
        }
        public FileResult Poster()
        {
            ViewBag.Message = "Your poster page.";
            var route = Server.MapPath("~/Documents/Poster.pdf");
            return File(route, "Poster.pdf");
        }
        public ActionResult Utility()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Boletin() {
            ViewBag.Message = "Information Boletin";
            return View();
        }

        public JsonResult Calculo(string peso)
        {
            
            try
            {
                var resul = new {
                    res = double.Parse(peso) * 2.17,
                    Ok = true
                };
                return Json(resul);
            }
            catch (Exception ex)
            {
                var resul = new
                {
                    res = ex.Message,
                    Ok = false
                };
                return Json(resul);
            }
        }

        public PartialViewResult Data() {
            var datas = new List<string>() { };
            datas.Add("1 Kg"); datas.Add("1 lb"); datas.Add("16 Oz");

            return PartialView("_Data", datas);

        }

        public JsonResult Form1(string txtCal) {
            Thread.Sleep(2000);
            bool IsEmail = new EmailAddressAttribute().IsValid(txtCal);
            if (IsEmail)
            {
                return Json("Email Valid");
            }
            return Json("Email InValid");
        }

        public PartialViewResult ActionLink()
        {
            //var datas = new List<string>() { };
            //datas.Add("1 Kg"); datas.Add("1 lb"); datas.Add("16 Oz");

            using (ApplicationDbContext db =new ApplicationDbContext())
            {
                var user = db.Users.Select(u => u.UserName).ToList();
                List<ControlViewModel> obj = new List<ControlViewModel>();
                for (int i = 0; i < user.Count; i++)
                {
                    ControlViewModel control = new ControlViewModel();
                    control.username = user[i];
                    obj.Add(control);
                }
                return PartialView("_User", obj);
            }            
        }
        
        public PartialViewResult userDetail(string user)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var userD = db.Users.Where(u => u.UserName == user).ToList();
                ControlViewModel control = new ControlViewModel();
                control.email = userD.ElementAt(0).Email;
                control.username = userD.ElementAt(0).UserName;
                return PartialView("_UserDetail", control);
            }
        }
        public class Persona
        {
            public string Name { get; set; }
            public DateTime Nacimento { get; set; }
        }       
    }
}