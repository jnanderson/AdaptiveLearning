using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{ 
    public class UserRoleController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /UserRole/
        [Authorize(Roles = "Administrator")]
        public ViewResult Index()
        {
            var roles = db.Roles.Include(r => r.Application);
            return View(roles.ToList());
        }

        //
        // GET: /UserRole/Details/5
        [Authorize(Roles = "Administrator")]
        public ViewResult Details(Guid id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // GET: /UserRole/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName");
            return View();
        } 

        //
        // POST: /UserRole/Create

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                role.RoleId = Guid.NewGuid();
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", role.ApplicationId);
            return View(role);
        }
        
        //
        // GET: /UserRole/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Guid id)
        {
            Role role = db.Roles.Find(id);
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", role.ApplicationId);
            return View(role);
        }

        //
        // POST: /UserRole/Edit/5

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationId = new SelectList(db.Applications, "ApplicationId", "ApplicationName", role.ApplicationId);
            return View(role);
        }

        //
        // GET: /UserRole/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(Guid id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

        //
        // POST: /UserRole/Delete/5

        [HttpPost, ActionName("Delete"), Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}