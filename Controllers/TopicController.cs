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
    public class TopicController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        //
        // GET: /Topic/

        public ViewResult Index()
        {
            return View(db.Topics.ToList());
        }

        //
        // GET: /Topic/Details/5

        public ViewResult Details(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }

        //
        // GET: /Topic/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Topic/Create

        [HttpPost]
        public ActionResult Create(Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(topic);
        }
        
        //
        // GET: /Topic/Edit/5
 
        public ActionResult Edit(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }

        //
        // POST: /Topic/Edit/5

        [HttpPost]
        public ActionResult Edit(Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(topic);
        }

        //
        // GET: /Topic/Delete/5
 
        public ActionResult Delete(int id)
        {
            Topic topic = db.Topics.Find(id);
            return View(topic);
        }

        //
        // POST: /Topic/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
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