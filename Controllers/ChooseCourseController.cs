using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{
    public class ChooseCourseController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();
        //
        // GET: /ChooseCourse/

        public ActionResult Index()
        {
           
            ViewBag.TopicId = new SelectList(db.Topics, "TopicId", "TopicName");
            return View();
        }

        [HttpPost]
        public ActionResult Index(ChooseCourse viewModel)
        {


            int selectedItem = viewModel.TopicId;
            
            return RedirectToAction("Index", "ShowTest", new { selectedItem = selectedItem });
        }

    }
}
