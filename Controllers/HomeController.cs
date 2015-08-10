using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdaptiveLearningFinal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Adaptive Learning Website";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
