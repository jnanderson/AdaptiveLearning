using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{
    public class ShowCourseTestController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();
        private static Questions myView = new Questions();
        private static int counter;

        public ActionResult Index(int SelectedItem)
        {

            counter = 0;
            var query = (from cq in db.CourseQuestions
                         where cq.ClassID == SelectedItem
                         select cq).ToList();

            var test = (from q in query
                        select new Questions()
                        {
                            QuestionID = q.QuestionID,
                            ClassID = q.ClassID,
                            Question = q.Question,
                            QuestionAnswerA = q.QuestionAnswerA,
                            QuestionAnswerB = q.QuestionAnswerB,
                            QuestionAnswerC = q.QuestionAnswerC,
                            QuestionAnswerD = q.QuestionAnswerD,
                            QuestionCorrectAnswer = q.QuestionCorrectAnswer,
                            QuestionExplanation = q.QuestionExplanation,
                            QuestionLevel = q.QuestionLevel,
                            Course = q.Course,
                            CourseResults = q.CourseResults
                        }).ToList();

            myView.SelectedItem = SelectedItem;
            //Error checking to ensure that there are questions in the database for a specific Topic
            if (test.Count() != 0)
            {
                myView.id = test[counter].QuestionID;
                return View(test[counter]);
            }
            else
            {
                return RedirectToAction("Index", "ChooseCourse");
            }
        }

        [HttpPost]
        public ActionResult Index(string Answer, int id, int SelectedItem)
        {

            UpdateTestResults(myView.id, Answer);

            var query = (from cq in db.CourseQuestions
                         where cq.ClassID == SelectedItem
                         select cq).ToList();

            var test = (from q in query
                        select new Questions()
                        {
                            QuestionID = q.QuestionID,
                            ClassID = q.ClassID,
                            Question = q.Question,
                            QuestionAnswerA = q.QuestionAnswerA,
                            QuestionAnswerB = q.QuestionAnswerB,
                            QuestionAnswerC = q.QuestionAnswerC,
                            QuestionAnswerD = q.QuestionAnswerD,
                            QuestionCorrectAnswer = q.QuestionCorrectAnswer,
                            QuestionExplanation = q.QuestionExplanation,
                            QuestionLevel = q.QuestionLevel,
                            Course = q.Course,
                            CourseResults = q.CourseResults
                        }).ToList();

            counter++;

            if (counter + 1 < test.Count())
            {
                myView.id = test[counter].QuestionID;
                return View(test[counter]);
            }
            else if (test.Count() == counter + 1)
            {
                myView.id = test[counter].QuestionID;
                return View("EndCourseTest", test[counter]);
            }
            else if (test.Count() < counter + 1)
            {
                return RedirectToAction("AfterTest", "EvaluationResults", new { SelectedItem = SelectedItem });
            }

            return View();
        }


        public void UpdateTestResults(int id, string Answer)
        {
            var query = (from c in db.CourseQuestions
                         where c.QuestionID == id && c.QuestionCorrectAnswer == Answer
                         select c).Any();

            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var evalResult = new EvaluationResult() { EvalQuestionID = id, Correct = query, UserID = UID };
            db.EvaluationResults.Add(evalResult);
            db.SaveChanges();



        }


    }
}
