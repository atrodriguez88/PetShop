using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShopModel;
using PetShopWeb.ViewModels;

namespace PetShopWeb.Controllers
{
    public class UtilesController : Controller
    {
        private ContextModel db = new ContextModel();

        // GET: Utiles
        public ActionResult Index(int pagina = 1)
        {
            var test = db.UtilesS.Include(m => m.TipoDeMascota).ToString();
            //return View(mascotas.ToList());

            var cantidadRegistrosPorPagina = 6; // parámetro
            using (var db = new ContextModel())
            {
                var utiles = db.UtilesS.OrderBy(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.UtilesS.Count();

                var modelo = new IndexViewModel();
                modelo.Utiles = utiles;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;

                return View(modelo);
            }
        }

        // GET: Utiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utiles utiles = db.UtilesS.Find(id);
            if (utiles == null)
            {
                return HttpNotFound();
            }
            return View(utiles);
        }

        // GET: Utiles/Create
        public ActionResult Create()
        {
            if (User.IsInRole("admin"))
            {
                ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name");
                return View();
            }
            return RedirectToAction("Register", "Account");
        }

        // POST: Utiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Img,Cost,Price,Description,BuyTime,IdTipo")] Utiles utiles)
        {
            if (ModelState.IsValid)
            {
                db.UtilesS.Add(utiles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", utiles.IdTipo);
            return View(utiles);
        }

        // GET: Utiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utiles utiles = db.UtilesS.Find(id);
                if (utiles == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", utiles.IdTipo);
                return View(utiles);
            }
            return RedirectToAction("Register", "Account");
        }

        // POST: Utiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Img,Cost,Price,Description,BuyTime,IdTipo")] Utiles utiles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utiles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", utiles.IdTipo);
            return View(utiles);
        }

        // GET: Utiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Utiles utiles = db.UtilesS.Find(id);
                if (utiles == null)
                {
                    return HttpNotFound();
                }
                return View(utiles);
            }
            return RedirectToAction("Register", "Account");
        }

        // POST: Utiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Utiles utiles = db.UtilesS.Find(id);
            db.UtilesS.Remove(utiles);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Filter(string filter)
        {
            if (filter.ToLower() == "all")
            {
                return RedirectToAction("Index");
            }
            var utiles = db.UtilesS.Include(m => m.TipoDeMascota).Where(s => s.TipoDeMascota.Name == filter).ToList();
            var totalDeRegistros = utiles.Count();

            var modelo = new IndexViewModel();
            modelo.Utiles = utiles;
            modelo.PaginaActual = 1;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = 6;

            return View("Index", modelo);
            ;
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
