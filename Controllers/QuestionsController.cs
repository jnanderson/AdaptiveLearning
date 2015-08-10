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
    public class QuestionsController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /Questions/
        [Authorize(Roles = "Administrator")]
        public ViewResult Index()
        {
            var coursequestions = db.CourseQuestions.Include(c => c.Course);
            return View(coursequestions.ToList());
        }

        //
        // GET: /Questions/Details/5
        [Authorize(Roles = "Administrator")]
        public ViewResult Details(int id)
        {
            CourseQuestion coursequestion = db.CourseQuestions.Find(id);
            return View(coursequestion);
        }

        //
        // GET: /Questions/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName");
            return View();
        } 

        //
        // POST: /Questions/Create

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Create(CourseQuestion coursequestion)
        {
            if (ModelState.IsValid)
            {
                db.CourseQuestions.Add(coursequestion);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName", coursequestion.ClassID);
            return View(coursequestion);
        }
        
        //
        // GET: /Questions/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            CourseQuestion coursequestion = db.CourseQuestions.Find(id);
            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName", coursequestion.ClassID);
            return View(coursequestion);
        }

        //
        // POST: /Questions/Edit/5

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Edit(CourseQuestion coursequestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursequestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.Courses, "ClassID", "ClassName", coursequestion.ClassID);
            return View(coursequestion);
        }

        //
        // GET: /Questions/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            CourseQuestion coursequestion = db.CourseQuestions.Find(id);
            return View(coursequestion);
        }

        //
        // POST: /Questions/Delete/5

        [HttpPost, ActionName("Delete"), Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CourseQuestion coursequestion = db.CourseQuestions.Find(id);
            db.CourseQuestions.Remove(coursequestion);
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