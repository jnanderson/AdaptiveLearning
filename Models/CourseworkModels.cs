using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdaptiveLearningFinal.Models
{
    public partial class Questions
    {

            public Questions()
            {
                this.CourseResults = new HashSet<CourseResult>();
            }

            public int QuestionID { get; set; }
            public Nullable<int> QuestionLevel { get; set; }
            public string QuestionAnswerA { get; set; }
            public string QuestionAnswerB { get; set; }
            public string QuestionAnswerC { get; set; }
            public string QuestionAnswerD { get; set; }
            public string QuestionExplanation { get; set; }
            public string Question { get; set; }
            public Nullable<int> ClassID { get; set; }
            public string QuestionCorrectAnswer { get; set; }

            public virtual Course Course { get; set; }
            public virtual ICollection<CourseResult> CourseResults { get; set; }

        
        
    }

    public partial class ChooseCourse
    {
        public ChooseCourse()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }


        public string SelectedItemValue { get; set; }

        
    }

    public partial class TestResults
    {
        public int CourseResultID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<bool> Correct { get; set; }

        public virtual CourseQuestion CourseQuestion { get; set; }
        public virtual User User { get; set; }
    }

   
}