using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShopWeb.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetShopWeb.Models;
using PetShopModel;

namespace PetShopWeb.Controllers
{
    [RoutePrefix("Mascotas")]
    [Authorize()]
    public class MascotasController : Controller
    {
        private ContextModel db = new ContextModel();

        [Route("")]
        [Route("Index")]
        // GET: Mascotas
        [AllowAnonymous]
        public ActionResult Index(int pagina = 1)
        {
            #region Test
            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //roleManager.Create(new IdentityRole("admin"));
            //var users = userManager.Users.Select(u => u.UserName).ToList();

            //var test = db.Mascotas.Include(m => m.TipoDeMascota).ToString();
            #endregion

            var cantidadRegistrosPorPagina = 6; // parámetro
            using (var db = new ContextModel())
            {
                var personas = db.Mascotas.OrderBy(x => x.Id)
                    .Skip((pagina - 1) * cantidadRegistrosPorPagina)
                    .Take(cantidadRegistrosPorPagina).ToList();
                var totalDeRegistros = db.Mascotas.Count();

                var modelo = new ViewModels.IndexViewModel();
                modelo.Mascotas = personas;
                modelo.PaginaActual = pagina;
                modelo.TotalDeRegistros = totalDeRegistros;
                modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;

                return View(modelo);
            }
        }

        // GET: Mascotas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mascota mascota = db.Mascotas.Find(id);
            if (mascota == null)
            {
                return HttpNotFound();
            }
            return View(mascota);
        }

        // GET: Mascotas/Create        
        public ActionResult Create()
        {
            string userId = User.Identity.GetUserId();//using Microsoft.AspNet.Identity;
            if (User.IsInRole("admin"))
            {
                ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name");
                return View();
            }
            return RedirectToAction("Register", "Account");
        }

        // POST: Mascotas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Img,Gender,Cost,Price,Description,BuyTime,IdTipo")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                db.Mascotas.Add(mascota);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", mascota.IdTipo);
            return View(mascota);
        }

        // GET: Mascotas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Mascota mascota = db.Mascotas.Find(id);
                if (mascota == null)
                {
                    return HttpNotFound();
                }
                ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", mascota.IdTipo);
                return View(mascota);
            }
            return RedirectToAction("Register", "Account");
        }

        // POST: Mascotas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Img,Gender,Cost,Price,Description,BuyTime,IdTipo")] Mascota mascota)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mascota).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdTipo = new SelectList(db.TipoDeMascotas, "IdTipo", "Name", mascota.IdTipo);
            return View(mascota);
        }

        // GET: Mascotas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (User.IsInRole("admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Mascota mascota = db.Mascotas.Find(id);
                if (mascota == null)
                {
                    return HttpNotFound();
                }
                return View(mascota);
            }
            return RedirectToAction("Register", "Account");
        }

        // POST: Mascotas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mascota mascota = db.Mascotas.Find(id);
            db.Mascotas.Remove(mascota);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Filter(string filter)
        {
            if (filter.ToLower() == "all")
            {
                return RedirectToAction("Index");
            }
            var mascotas = db.Mascotas.Include(m => m.TipoDeMascota).Where(s => s.TipoDeMascota.Name == filter).ToList();
            var totalDeRegistros = mascotas.Count();

            var modelo = new ViewModels.IndexViewModel();
            modelo.Mascotas = mascotas;
            modelo.PaginaActual = 1;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = 6;

            return View("Index", modelo);
        }
        [Route("Raza")]
        [Route("Raza/{generos}")]
        [AllowAnonymous]
        public ActionResult FindBy(string generos = "all")
        {
            var array = generos.Split('/');
            

            var result = new List<Mascota>();
            if (generos == "all")
            {
                var all = db.Mascotas.Include(m => m.TipoDeMascota).ToList();
                result.AddRange(all);
            }
            for (int i = 0; i < array.Length; i++)
            {
                string type = array[i].ToString();
                if (type.ToLower() == "dog")
                {
                    var dog = db.Mascotas.Include(m => m.TipoDeMascota).Where(s => s.TipoDeMascota.Name == type).ToList();
                    result.AddRange(dog);
                }
                if (type.ToLower() == "cat")
                {
                    var cat = db.Mascotas.Include(m => m.TipoDeMascota).Where(s => s.TipoDeMascota.Name == type).ToList();
                    result.AddRange(cat);
                }
                if (type.ToLower() == "ave")
                {
                    var ave = db.Mascotas.Include(m => m.TipoDeMascota).Where(s => s.TipoDeMascota.Name == type).ToList();
                    result.AddRange(ave);
                }
            }

            var totalDeRegistros = result.Count();

            var modelo = new ViewModels.IndexViewModel();
            modelo.Mascotas = result;
            modelo.PaginaActual = 1;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = 6;

            return View(modelo);
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
