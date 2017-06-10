using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            #region Consumiendo Store Procedure
            //Solution 1
            //var namesMale = db.Database.SqlQuery<string>("sp_name @gender", new SqlParameter("@gender", "male")).ToList();
            //Solution 2
            //var namesM = db.Database.ExecuteSqlCommand("sp_name @gender", new SqlParameter("@gender", "male")); 
            #endregion

            #region Take Data the Web.Config
            var secureAppSettings = ConfigurationManager.GetSection("secureAppSettings") as NameValueCollection;
            var Data1 = secureAppSettings["Data1"];
            var Data2 = secureAppSettings["Data2"]; 
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

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Utility()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Calculo(double peso)
        {
            try
            {
                var res = peso * 2.17;
                return Json(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}