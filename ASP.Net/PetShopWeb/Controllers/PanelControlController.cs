using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PetShopModel;
using PetShopWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;

namespace PetShopWeb.Controllers
{
    public class PanelControlController : Controller
    {
        // GET: PanelControl
        public ActionResult Index()
        {
            using (ContextModel db = new ContextModel())
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                //roleManager.Create(new IdentityRole("admin"));

                var users = userManager.Users.Select(u => u.UserName).ToList();
                return View(users);
            }            
        }

        // GET: PanelControl/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PanelControl/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PanelControl/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PanelControl/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PanelControl/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PanelControl/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PanelControl/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
