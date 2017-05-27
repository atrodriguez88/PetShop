using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ClientesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clientes
        public ActionResult Index()
        {
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


            return View(db.Clientes.ToList());
        }
        [HttpPost]
        public JsonResult ver()
        {
            return Json(2 * 5);
        }
        [HttpPost]
        public JsonResult verAction(int nombre =2)
        {
            if (nombre != 2)
            {
                return Json(3 * 5);
            }
            return Json(nombre * 5);
        }
        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,email,confirEmail,phone,gender")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,phone,gender")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddPet(int id)
        {
            using (ApplicationDbContext con = new ApplicationDbContext() )
            {
                //var list1 = con.Mascotas.Where(c => c.Cliente.id == null).Select(c => new { Id = c.id, tipo = c.type });
                //foreach (var item in list1)
                //{
                //    int asd = item.Id;
                //    string asds = item.tipo;
                //}
                //string d = list1.ToList()[0].tipo;
                var list = con.Mascotas.Where(c => c.Cliente.id == null).Select(c => c.id);
                List<int> freePet = list.ToList();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
