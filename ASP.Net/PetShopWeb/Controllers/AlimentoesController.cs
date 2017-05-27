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
    [RoutePrefix("Alimentos")]
    public class AlimentoesController : Controller
    {
        private ContextModel db = new ContextModel();

        // GET: Alimentoes
        [Route("")]
        [Route("Index")]
        public ActionResult Index(int pagina = 1)
        {
            var test = db.Alimentos.Include(m => m.TipoDeMascota).ToString();
            //return View(mascotas.ToList());

            var cantidadRegistrosPorPagina = 6; // parámetro
            using (var db = new ContextModel())
            {
                var alimentos = db.Alimentos.OrderBy(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.Alimentos.Count();

                var modelo = new IndexViewModel();
                modelo.Alimentos = alimentos;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;

                return View(modelo);
            }
        }

        // GET: Alimentoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alimento alimento = db.Alimentos.Find(id);
            if (alimento == null)
            {
                return HttpNotFound();
            }
            return View(alimento);
        }

        // GET: Alimentoes/Create
        public ActionResult Create()
        {
            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name");
            return View();
        }

        // POST: Alimentoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cost,Price,Description,IdTipo")] Alimento alimento)
        {            
            if (ModelState.IsValid)
            {
                alimento.BuyTime = DateTime.Now;
                db.Alimentos.Add(alimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", alimento.IdTipo);
            return View(alimento);
        }

        // GET: Alimentoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alimento alimento = db.Alimentos.Find(id);
            if (alimento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", alimento.IdTipo);
            return View(alimento);
        }

        // POST: Alimentoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cost,Price,Description,IdTipo,BuyTime")] Alimento alimento)
        {
            if (ModelState.IsValid)
            {
                //alimento.BuyTime = DateTime.Now;
                db.Entry(alimento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", alimento.IdTipo);
            return View(alimento);
        }

        // GET: Alimentoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alimento alimento = db.Alimentos.Find(id);
            if (alimento == null)
            {
                return HttpNotFound();
            }
            return View(alimento);
        }

        // POST: Alimentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alimento alimento = db.Alimentos.Find(id);
            db.Alimentos.Remove(alimento);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Filter(string filter)
        {
            if (filter.ToLower() == "all")
            {
                return RedirectToAction("Index");
            }
            var alimentos = db.Alimentos.Include(a => a.TipoDeMascota).Where(s => s.TipoDeMascota.Name == filter).ToList();
            var totalDeRegistros = alimentos.Count();

            var modelo = new IndexViewModel();
            modelo.Alimentos = alimentos;
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
