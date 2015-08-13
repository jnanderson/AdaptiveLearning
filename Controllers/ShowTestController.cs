using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AdaptiveLearningFinal.Models;
using System.Data.Entity;

namespace AdaptiveLearningFinal.Controllers
{
    public class ShowTestController : Controller
    {
        //
        // GET: /ShowTest/
        private AdaptiveLearningEntities db = new AdaptiveLearningEntities();
       
        public ActionResult Index(int SelectedItem)
        {
            //var questions = db.CourseQuestions.ToList();

            var query = (from cq in db.CourseQuestions
                         join c in db.Courses on cq.ClassID equals c.ClassID
                         where c.TopicId == SelectedItem
                         group cq by new { ClassID = cq.ClassID } into g
                         select
                            g.OrderBy(e => e.ClassID).FirstOrDefault()).ToList();

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
                                
            
            return View(test[0]);
        }

        [HttpPost]
        public ActionResult NextQuestion(string Answer, int id)
        {
            
            bool correct = FindCorrectAnswer(Answer, id);
            UpdateTestResults(correct, id);

            return View();
        }

        private bool FindCorrectAnswer(string Answer, int id)
        {
            
            var query = (from c in db.CourseQuestions
                         where c.QuestionID == id && c.QuestionCorrectAnswer == Answer
                         select c).Any();

            return query;
            

        }

        private void UpdateTestResults(bool correct, int id)
        {
            Guid UID = (Guid)(System.Web.Security.Membership.GetUser().ProviderUserKey);
            var courseResult = new CourseResult() { QuestionID = id, Correct = correct, UserId = UID };
            db.CourseResults.Add(courseResult);
            db.SaveChanges();
                        
 
            
        }


    }
}
