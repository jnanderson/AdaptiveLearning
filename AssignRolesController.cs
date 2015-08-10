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
    public class AssignRolesController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /AssignRoles/
       [Authorize(Roles = "Administrator")]
        public ViewResult Index()
        {
            var usersinroles = db.UsersInRoles.Include(u => u.Role).Include(u => u.User);
            return View(usersinroles.ToList());
        }

        //
        // GET: /AssignRoles/Details/5
        [Authorize(Roles = "Administrator")]
        public ViewResult Details(int id)
        {
            UsersInRole usersinrole = db.UsersInRoles.Find(id);
            return View(usersinrole);
        }

        //
        // GET: /AssignRoles/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        } 

        //
        // POST: /AssignRoles/Create

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Create(UsersInRole usersinrole)
        {
            if (ModelState.IsValid)
            {
                db.UsersInRoles.Add(usersinrole);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", usersinrole.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", usersinrole.UserId);
            return View(usersinrole);
        }
        
        //
        // GET: /AssignRoles/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            UsersInRole usersinrole = db.UsersInRoles.Find(id);
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", usersinrole.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", usersinrole.UserId);
            return View(usersinrole);
        }

        //
        // POST: /AssignRoles/Edit/5

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Edit(UsersInRole usersinrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersinrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", usersinrole.RoleId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", usersinrole.UserId);
            return View(usersinrole);
        }

        //
        // GET: /AssignRoles/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            UsersInRole usersinrole = db.UsersInRoles.Find(id);
            return View(usersinrole);
        }

        //
        // POST: /AssignRoles/Delete/5

        [HttpPost, ActionName("Delete"), Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {            
            UsersInRole usersinrole = db.UsersInRoles.Find(id);
            db.UsersInRoles.Remove(usersinrole);
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