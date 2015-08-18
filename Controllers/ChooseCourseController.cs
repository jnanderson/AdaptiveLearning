using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{
    /// <summary>
    /// The ChooseCourse controller controls the functions of choosing a topic and choosing a course
    /// </summary>
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
        /// <summary>
        /// This returns both the test view and the show available classes view
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(ChooseCourse viewModel)
        {

            int selectedItem = viewModel.TopicId;

            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);

            //query returns true or false.  Compares the topicID selected and whether or not the Pretest was completed  
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
        /// <summary>
        /// This is the controller that will be used if a pre-test has not been completed.
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        public ActionResult PreTestShowAvailableClasses(int SelectedItem)
        {
            UpdateTestResults(SelectedItem); //Method that updates when the pre-test is complete, and inserts the results into the results table
            List<Course> select = GetCourses(SelectedItem); //Returns available courses based on Pre-test answers

            //Passes the DbContext Course to drop down list
            ViewBag.ClassID = new SelectList(select, "ClassID", "ClassName");

            return View("ShowAvailableClasses");

        }
        /// <summary>
        /// This ActionResult shows the available classes to the student based on their answers to the pre-test
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        public ActionResult ShowAvailableClasses(int SelectedItem)
        {

            List<Course> select = GetCourses(SelectedItem);
            ViewBag.ClassID = new SelectList(select, "ClassID", "ClassName");
    
            return View();

        }
        /// <summary>
        /// This ActionResult shows the available classes to the student based on their answers to the pre-test
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ShowAvailableClasses(ClassModel viewModel)
        {
            int selectedClass = viewModel.ClassID;

            return RedirectToAction("ClassView", "ChooseCourse", new { selectedItem = selectedClass });

        }
        /// <summary>
        /// ClassView ActionResult provides the controls for the coursework page (i.e. Video URL and Website URLs to be served to the user via the database
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns>View(viewModel)</returns>
        public ActionResult ClassView(int selectedItem)
        {
            LearningModel viewModel = new LearningModel();
            string Material = GetMaterial(selectedItem);
            ViewBag.MaterialID = Material;
            viewModel.ClassID = selectedItem;
            return View(viewModel);
        }

        /// <summary>
        /// Function that updates the Test Results based on the User's selections
        /// </summary>
        /// <param name="SelectedItem"></param>
        public void UpdateTestResults(int SelectedItem)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var usertopic = new UserTopic { TopicId = SelectedItem, UserId = UID, PretestComplete = true };
            db.UserTopics.Add(usertopic);
            db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Provides the user with the correct type of Learning method for their coursework
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        public String GetMaterial(int SelectedItem)
        {
            //UserID from the User Table
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);

            //Returns the LearningStyleName (Video, Example) based on the Users selection
            var query = (from c in db.CourseResults
                         join d in db.CourseQuestions on c.QuestionID equals d.QuestionID
                         join e in db.CourseMaterials on d.ClassID equals e.ClassID
                         join f in db.Users on c.UserId equals f.UserId
                         join g in db.LearningStyles on f.LearningStyleID equals g.LearningStyleID
                         where e.ClassID == SelectedItem && f.UserId == UID
                         select g.LearningStyleName).First().ToString();

            if(query == "Video")
            {
                //Provides the correct Video for the course that was selected
                var Video = (from c in db.CourseMaterials
                                where c.ClassID == SelectedItem
                                select c.VideoUrl).First().ToString();
                return Video;
               

            }
            else
            {
                //Provides the correct website for the course that was selected
                var Reading = (from c in db.CourseMaterials
                                where c.ClassID == SelectedItem
                                select c.ReadingUrl).First().ToString();
                return Reading;

            }
            
        }


    }
}
