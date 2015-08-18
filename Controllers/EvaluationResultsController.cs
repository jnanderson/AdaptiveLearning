using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{
    /// <summary>
    ///  This class performs pulls and processes the results from the Course Test, and calculates the percentage correct for the user
    /// </summary>
    public class EvaluationResultsController : Controller
    {

        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();

        /// <summary>
        /// The Index ActionResult serves to provide a list of courses that a user has taken a test, and will populate a dropdown list with those courses
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);

            //Provides a list of all the courses that the user has taken a test for
            var query = (from a in db.EvaluationResults
                         join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                         join c in db.Courses on b.ClassID equals c.ClassID
                         where a.UserID == UID
                         group c by c.ClassID into g
                         select new CoursesTakenModel()
                         {
                             ClassID = g.Key,
                             ClassName = g.Select(n=>n.ClassName).FirstOrDefault()

                         }).ToList();

            ViewBag.ClassID = new SelectList(query, "ClassID", "ClassName");
            return View();
        }

        /// <summary>
        /// Redirects the posted data to the ViewResults action in the EvaluationResults controller with the selected ClassID
        /// </summary>
        /// <param name="myView"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(ClassModel myView)
        {
            int SelectedItem = myView.ClassID;

            return RedirectToAction("ViewResults", "EvaluationResults", new { SelectedItem = SelectedItem });
        }

        /// <summary>
        /// The ViewResults ActionResult serves to calculate statistics and control the courses that are available to be selected
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        public ActionResult ViewResults(int SelectedItem)
        {
            //Check to make sure that a valid course has been selected
            if (SelectedItem == 0)
            {
                Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);

                //Provides a list of the ClassIDs and ClassNames from the course based on the UserID
                var query = (from a in db.EvaluationResults
                             join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                             join c in db.Courses on b.ClassID equals c.ClassID
                             where a.UserID == UID
                             group c by c.ClassID into g
                             select new CoursesTakenModel()
                             {
                                 ClassID = g.Key,
                                 ClassName = g.Select(n => n.ClassName).FirstOrDefault()

                             }).ToList();
                ViewBag.ClassID = new SelectList(query, "ClassID", "ClassName");
                return View("Index");
            }
            //Valid course has been selected
            else
            {
                List<ResultsModel> query = CalculateStatistics(SelectedItem);
                return View(query);
            }
        }

        /// <summary>
        /// This retrieves the Users test from the DB and returns a list view for the index page
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns>AfterTest View containing the query List</returns>
        public ActionResult AfterTest(int SelectedItem)
        {
            List<ResultsModel> query = CalculateStatistics(SelectedItem);
            UpdateDatabase(query);            
            return View(query);
        }

        /// <summary>
        /// Method Calculates the number of correct answers divided by the total number of answers.  This is a cumulative affect
        /// </summary>
        /// <param name="SelectedItem"></param>
        public List<ResultsModel> CalculateStatistics(int SelectedItem)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            float x;
            float y;

            //Provides a count of the number of correct answers from the course test
            var correct = (from a in db.EvaluationResults
                           join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                           where a.UserID == UID && b.ClassID == SelectedItem && a.Correct == true
                           group a by a.UserID into g
                           select new { NumCorrect = g.Count() }).FirstOrDefault();

            //Check to make sure that correct is not null and if it is set the value x = 0 otherwise use the value from correct to set x
            if (correct == null)
            {
                x = 0;
            }
            else
            {
                x = correct.NumCorrect;
            }

            //Provides a count of the number of total answers from the course test
            var total = (from a in db.EvaluationResults
                           join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                           where a.UserID == UID && b.ClassID == SelectedItem
                           group a by a.UserID into g
                           select new { NumCorrect = g.Count() }).FirstOrDefault();
   
            y = total.NumCorrect;
          
            
            float Percentage = (x / y) * 100; 

            //Creates a new model that stores course test information with percentage correct
            var query = (from a in db.EvaluationResults
                         join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                         join c in db.Courses on b.ClassID equals c.ClassID
                         where a.UserID == UID && b.ClassID == SelectedItem
                         select new ResultsModel()
                         {
                             Correct = a.Correct,
                             Question = b.Question,
                             QuestionCorrectAnswer = b.QuestionCorrectAnswer,
                             QuestionExplanation = b.QuestionExplanation,
                             ClassName = c.ClassName,
                             CountCorrect = x,
                             CountTotal = total.NumCorrect,
                             PercentageCorrect = Percentage


                         }).ToList();



            return query;

        }
        /// <summary>
        /// Function that will update the CourseResult table when the percentage correct of a course test is greater than 69.  This will set the Correct Attribute = true and the course will not show up in the drop down anymore.
        /// </summary>
        /// <param name="query"></param>
        public void UpdateDatabase(List<ResultsModel> query)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            string NewClassName = query[0].ClassName;
            string NewCorrect = query[0].Correct.ToString();

            //Take the first instance of query and check the Percentage correct (PercentageCorrect will be the same for each entry in the list
            if (query[0].PercentageCorrect > 69)
            {
                var update = (from a in db.Courses
                              join b in db.CourseQuestions on a.ClassID equals b.ClassID
                              join c in db.CourseResults on b.QuestionID equals c.QuestionID
                              where c.UserId == UID && a.ClassName == NewClassName
                              select c).ToList();

                //Check to make sure that the update list is not empty
                if (update.Count() != 0)
                {
                    var Updater = update.FirstOrDefault();
                    Updater.Correct = true;
                    db.SaveChanges();
                }



            }

        }

    }
}
