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
    public class ResultsController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /Results/

        public ViewResult Index()
        {
            var courseresults = db.CourseResults.Include(c => c.CourseQuestion).Include(c => c.User);
            return View(courseresults.ToList());
        }

        //
        // GET: /Results/Details/5

        public ViewResult Details(int id)
        {
            CourseResult courseresult = db.CourseResults.Find(id);
            return View(courseresult);
        }

        //
        // GET: /Results/Create

        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.CourseQuestions, "QuestionID", "QuestionAnswer");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        } 

        //
        // POST: /Results/Create

        [HttpPost]
        public ActionResult Create(CourseResult courseresult)
        {
            if (ModelState.IsValid)
            {
                db.CourseResults.Add(courseresult);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.QuestionID = new SelectList(db.CourseQuestions, "QuestionID", "QuestionAnswer", courseresult.QuestionID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", courseresult.UserId);
            return View(courseresult);
        }
        
        //
        // GET: /Results/Edit/5
 
        public ActionResult Edit(int id)
        {
            CourseResult courseresult = db.CourseResults.Find(id);
            ViewBag.QuestionID = new SelectList(db.CourseQuestions, "QuestionID", "QuestionAnswer", courseresult.QuestionID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", courseresult.UserId);
            return View(courseresult);
        }

        //
        // POST: /Results/Edit/5

        [HttpPost]
        public ActionResult Edit(CourseResult courseresult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseresult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.CourseQuestions, "QuestionID", "QuestionAnswer", courseresult.QuestionID);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", courseresult.UserId);
            return View(courseresult);
        }

        //
        // GET: /Results/Delete/5
 
        public ActionResult Delete(int id)
        {
            CourseResult courseresult = db.CourseResults.Find(id);
            return View(courseresult);
        }

        //
        // POST: /Results/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            CourseResult courseresult = db.CourseResults.Find(id);
            db.CourseResults.Remove(courseresult);
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