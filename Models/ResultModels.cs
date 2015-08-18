using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdaptiveLearningFinal.Models
{
    /// <summary>
    /// A model that represents a version of the EvaluationResult Table
    /// </summary>
    public partial class ResultsModel
    {
        public int ERID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<int> EvalQuestionID { get; set; }
        public Nullable<bool> Correct { get; set; }
        public string QuestionCorrectAnswer { get; set; }
        public string Question { get; set; }
        public string QuestionExplanation { get; set; }
        public string ClassName { get; set; }

        public virtual CourseQuestion CourseQuestion { get; set; }
        public virtual User User { get; set; }

        public float CountCorrect { get; set; }
        public int CountTotal { get; set; }
        public float PercentageCorrect { get; set; }

       
        
    }
    /// <summary>
    /// A model that represents a version of the Courses Table
    /// </summary>
    public partial class CoursesTakenModel
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string SelectedItem { get; set; }

    }
}