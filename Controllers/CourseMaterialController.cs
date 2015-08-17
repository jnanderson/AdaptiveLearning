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
    public class CourseMaterialController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /CourseMaterial/

        public ViewResult Index()
        {
            var coursematerials = db.CourseMaterials.Include(c => c.Course);
            return View(coursematerials.ToList());
        }

        //
        // GET: /CourseMaterial/Details/5

        public ViewResult Details(int id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            return View(coursematerial);
        }

        //
        // GET: /CourseMaterial/Create

        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName");
            return View();
        } 

        //
        // POST: /CourseMaterial/Create

        [HttpPost]
        public ActionResult Create(CourseMaterial coursematerial)
        {
            if (ModelState.IsValid)
            {
                db.CourseMaterials.Add(coursematerial);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName", coursematerial.ClassID);
            return View(coursematerial);
        }
        
        //
        // GET: /CourseMaterial/Edit/5
 
        public ActionResult Edit(int id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName", coursematerial.ClassID);
            return View(coursematerial);
        }

        //
        // POST: /CourseMaterial/Edit/5

        [HttpPost]
        public ActionResult Edit(CourseMaterial coursematerial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursematerial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName", coursematerial.ClassID);
            return View(coursematerial);
        }

        //
        // GET: /CourseMaterial/Delete/5
 
        public ActionResult Delete(int id)
        {
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            return View(coursematerial);
        }

        //
        // POST: /CourseMaterial/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CourseMaterial coursematerial = db.CourseMaterials.Find(id);
            db.CourseMaterials.Remove(coursematerial);
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