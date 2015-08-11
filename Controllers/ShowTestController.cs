using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{
    public class ShowTestController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();
        //
        // GET: /ShowTest/

        public ActionResult Index()
        {
            var questions = db.CourseQuestions.ToList();
            return View(questions);
        }

        public ActionResult NextQuestion()
        {
            return View();
        }

    }
}
