using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdaptiveLearningFinal.Models;

namespace AdaptiveLearningFinal.Controllers
{
    /// <summary>
    /// The ShowTestController provides functionality for allowing a user to view the test for the course
    /// </summary>
    public class ShowCourseTestController : Controller
    {
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();
        private static Questions myView = new Questions();
        private static int counter;

        /// <summary>
        /// Provides a list of the questions that will be asked to the user based on the course that they have selected
        /// </summary>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        public ActionResult Index(int SelectedItem)
        {
            //counter is used to keep track of the number of questions
            counter = 0;

            //Provides the selected course (ClassID)
            var query = (from cq in db.CourseQuestions
                         where cq.ClassID == SelectedItem
                         select cq).ToList();

            //Provides the questions for the course based on the query from the previous linq statement
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

        /// <summary>
        /// Post method that takes the responses from the Index view and updates the database and serves the next question in the list
        /// </summary>
        /// <param name="Answer"></param>
        /// <param name="id"></param>
        /// <param name="SelectedItem"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(string Answer, int id, int SelectedItem)
        {
            //Function that updates the CourseResult table with the user Answer and ClassID
            UpdateTestResults(myView.id, Answer);

            //Requery for the ClassID
            var query = (from cq in db.CourseQuestions
                         where cq.ClassID == SelectedItem
                         select cq).ToList();

            //Requery using the ClassID to get the questions for the test
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

            //Increment of counter to move to the next question
            counter++;

            //Check of counter to provide the correct button at the end of the View (Next Question)
            if (counter + 1 < test.Count())
            {
                myView.id = test[counter].QuestionID;
                return View(test[counter]);
            }
            //Check of counter to provide the correct button at the end of the View (Submit)
            else if (test.Count() == counter + 1)
            {
                myView.id = test[counter].QuestionID;
                return View("EndCourseTest", test[counter]);
            }
            //Check to send the user to the Evaluation Results page, to see their scores
            else if (test.Count() < counter + 1)
            {
                return RedirectToAction("AfterTest", "EvaluationResults", new { SelectedItem = SelectedItem });
            }

            return View();
        }

        /// <summary>
        /// Function that Updates the EvaluationResult table with whether the answer is correct or not.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Answer"></param>
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
