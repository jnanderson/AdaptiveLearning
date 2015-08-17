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
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var query = (from c in db.UserTopics
                         where c.UserId == UID && c.TopicId == selectedItem && c.PretestComplete == true
                         select c).Any();

            if (query == true)
            {
                return RedirectToAction("ShowAvailableClasses", "ChooseCourse", new { selectedItem = selectedItem });
            }
            else
            {
            
                return RedirectToAction("Index", "ShowTest", new { selectedItem = selectedItem });
            }


        }
        //This is a controller that will be used if the user has to take a pre-test
        public ActionResult PreTestShowAvailableClasses(int SelectedItem)
        {
            UpdateTestResults(SelectedItem);
            List<Course> select = GetCourses(SelectedItem);

            //Passes the DbContext Course to drop down list
            ViewBag.ClassID = new SelectList(select, "ClassID", "ClassName");

            return View("ShowAvailableClasses");

        }
        
        public ActionResult ShowAvailableClasses(int SelectedItem)
        {

            List<Course> select = GetCourses(SelectedItem);
            ViewBag.ClassID = new SelectList(select, "ClassID", "ClassName");
    
            return View();

        }

        [HttpPost]
        public ActionResult ShowAvailableClasses(ClassModel viewModel)
        {
            int selectedClass = viewModel.ClassID;

            return RedirectToAction("ClassView", "ChooseCourse", new { selectedItem = selectedClass });

        }

        public ActionResult ClassView(int selectedItem)
        {
            LearningModel viewModel = new LearningModel();
            string Material = GetMaterial(selectedItem);
            ViewBag.MaterialID = Material;
            viewModel.ClassID = selectedItem;
            return View(viewModel);
        }


        public void UpdateTestResults(int SelectedItem)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var usertopic = new UserTopic { TopicId = SelectedItem, UserId = UID, PretestComplete = true };
            db.UserTopics.Add(usertopic);
            db.SaveChanges();
        }

        public List<Course> GetCourses(int SelectedItem)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var query = (from c in db.CourseResults
                         join d in db.CourseQuestions on c.QuestionID equals d.QuestionID
                         join e in db.Courses on d.ClassID equals e.ClassID
                         where c.Correct == false && c.UserId == UID
                         select e).ToList();
            return query;

        }

        public String GetMaterial(int SelectedItem)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var query = (from c in db.CourseResults
                         join d in db.CourseQuestions on c.QuestionID equals d.QuestionID
                         join e in db.CourseMaterials on d.ClassID equals e.ClassID
                         join f in db.Users on c.UserId equals f.UserId
                         join g in db.LearningStyles on f.LearningStyleID equals g.LearningStyleID
                         where e.ClassID == SelectedItem && f.UserId == UID
                         select g.LearningStyleName).First().ToString();

            if(query == "Video")
            {
                var Video = (from c in db.CourseMaterials
                                where c.ClassID == SelectedItem
                                select c.VideoUrl).First().ToString();
                return Video;
               

            }
            else
            {
                var Reading = (from c in db.CourseMaterials
                                where c.ClassID == SelectedItem
                                select c.ReadingUrl).First().ToString();
                return Reading;

            }
            
        }


    }
}
