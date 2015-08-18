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
    /// <summary>
    /// The ClassController is an administrative controller that provides for the Creation, Edit, Detail, and Delete functions for specific Topics
    /// </summary>
    public class ClassController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /Class/
        [Authorize(Roles = "Administrator")]
        public ViewResult Index()
        {
            var courses = db.Courses.Include(c => c.Topic);
            return View(courses.ToList());
        }

        //
        // GET: /Class/Details/5
        [Authorize(Roles = "Administrator")]
        public ViewResult Details(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }

        //
        // GET: /Class/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName");
            return View();
        } 

        //
        // POST: /Class/Create

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", course.TopicId);
            return View(course);
        }
        
        //
        // GET: /Class/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            Course course = db.Courses.Find(id);
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", course.TopicId);
            return View(course);
        }

        //
        // POST: /Class/Edit/5

        [HttpPost, Authorize(Roles = "Administrator")]
        public ActionResult Edit(Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName", course.TopicId);
            return View(course);
        }

        //
        // GET: /Class/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            Course course = db.Courses.Find(id);
            return View(course);
        }

        //
        // POST: /Class/Delete/5

        [HttpPost, ActionName("Delete"), Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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