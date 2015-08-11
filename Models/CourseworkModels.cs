using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptiveLearningFinal.Models
{
    public class Questions
    {

            public Questions()
            {
                this.CourseResults = new HashSet<CourseResult>();
            }

            public int QuestionID { get; set; }
            public Nullable<int> QuestionLevel { get; set; }
            public string QuestionAnswer { get; set; }
            public string QuestionExplanation { get; set; }
            public string Question { get; set; }
            public Nullable<int> ClassID { get; set; }

            public virtual Course Course { get; set; }
            public virtual ICollection<CourseResult> CourseResults { get; set; }
        
    }

    public class ChooseCourse
    {
        public ChooseCourse()
        {
            this.Courses = new HashSet<Course>();
        }
    
        public int TopicId { get; set; }
        public string TopicName { get; set; }
        public string TopicDescription { get; set; }
    
        public virtual ICollection<Course> Courses { get; set; }
    }
}