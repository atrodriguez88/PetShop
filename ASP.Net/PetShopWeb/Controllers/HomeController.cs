using PetShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public class Persona
        {
            public string Name { get; set; }
            public DateTime Nacimento { get; set; }
        }       
    }
}