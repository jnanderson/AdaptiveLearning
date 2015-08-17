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

        public ActionResult Index()
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
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

        [HttpPost]
        public ActionResult Index(ClassModel myView)
        {
            int SelectedItem = myView.ClassID;

            return RedirectToAction("ViewResults", "EvaluationResults", new { SelectedItem = SelectedItem });
        }


        public ActionResult ViewResults(int SelectedItem)
        {
            List<ResultsModel> query = CalculateStatistics(SelectedItem);
            return View(query);

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

            var correct = (from a in db.EvaluationResults
                           join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                           where a.UserID == UID && b.ClassID == SelectedItem && a.Correct == true
                           group a by a.UserID into g
                           select new { NumCorrect = g.Count() }).FirstOrDefault();

            if (correct == null)
            {
                x = 0;
            }
            else
            {
                x = correct.NumCorrect;
            }

            var total = (from a in db.EvaluationResults
                           join b in db.CourseQuestions on a.EvalQuestionID equals b.QuestionID
                           where a.UserID == UID && b.ClassID == SelectedItem
                           group a by a.UserID into g
                           select new { NumCorrect = g.Count() }).First();

            float y = total.NumCorrect;
            float Percentage = (x / y) * 100; 


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

        public void UpdateDatabase(List<ResultsModel> query)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            string NewClassName = query[0].ClassName;
            string NewCorrect = query[0].Correct.ToString();

            if (query[0].PercentageCorrect > 69)
            {
                var update = (from a in db.Courses
                              join b in db.CourseQuestions on a.ClassID equals b.ClassID
                              join c in db.CourseResults on b.QuestionID equals c.QuestionID
                              where c.UserId == UID && a.ClassName == NewClassName
                              select c).ToList();

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
