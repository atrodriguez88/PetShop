using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PetShopWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetShopWeb.ViewModels;

namespace PetShopWeb.Controllers
{
    //[Authorize(Roles ="super")]
    [RoutePrefix("Control")]
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        [Route("")]
        [Route("Index")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: ApplicationUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public ActionResult Create()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            ViewBag.Rol = new SelectList(roleManager.Roles.Select(r => r.Name).ToList());
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ControlViewModel control)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.Values.ElementAt(0).Value.AttemptedValue !="" &&
                    ModelState.Values.ElementAt(1).Value.AttemptedValue != "" &&
                    ModelState.Values.ElementAt(2).Value.AttemptedValue != "")
                {
                    using (ApplicationDbContext db = new ApplicationDbContext())
                    {
                        var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                        var user = new ApplicationUser() {
                            UserName = control.username,
                    };
                        var result = userManager.Create(user, control.password);     
                                         
                        if (result.Succeeded)
                        {
                            var result2 = userManager.AddToRole(userManager.FindByName(user.UserName).Id, control.Rol);
                            return RedirectToAction("Index");
                        }

                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                        ViewBag.Rol = new SelectList(roleManager.Roles.Select(r => r.Name).ToList());
                        return View(control);
                    }
                }
                return RedirectToAction("Create");
            }
            return RedirectToAction("Register", "Account");
            
        }

        // GET: ApplicationUsers/Edit/5
        public ActionResult Edit(string id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    if (db.Users.Find(id) == null)
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                        var control = new ControlViewModel()
                        {
                            email = db.Users.Find(id).Email,
                            //password = db.Users.Find(id).PasswordHash,
                            username = db.Users.Find(id).UserName,
                            Rol = roleManager.FindById(db.Users.Find(id).Roles.FirstOrDefault().RoleId).Name
                        };
                        ViewBag.Rol = new SelectList(roleManager.Roles.Select(r => r.Name).ToList());
                        return View(control);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Index");
            }
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(ControlViewModel control)
        //{
        //        db.Entry(applicationUser).State = EntityState.Modified;
        //        db.SaveChanges();
        //    //**********************************************
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        if (ModelState.Values.ElementAt(0).Value.AttemptedValue != "" &&
        //            ModelState.Values.ElementAt(1).Value.AttemptedValue != "")
        //        {
        //            using (ApplicationDbContext db = new ApplicationDbContext())
        //            {
        //                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                                                
        //                var user = userManager.FindById(applicationUser.Id);
        //                user.Email = applicationUser.Email;
        //                user.PhoneNumber = applicationUser.PhoneNumber;
        //                //user.Roles = applicationUser.Roles;
        //               // if (result.Succeeded)
        //                {
        //                    return RedirectToAction("Index");
        //                }
        //                return View(applicationUser);
        //            }
        //        }
        //        return View(applicationUser);
        //    }
        //    return RedirectToAction("Register", "Account");
        //}

        // GET: ApplicationUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);
            db.Users.Remove(applicationUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddRol()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRol(string roleName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

                roleManager.Create(new IdentityRole(roleName));
            }
            return RedirectToAction("Index");
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