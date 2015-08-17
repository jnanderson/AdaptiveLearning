//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdaptiveLearningFinal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CourseQuestion
    {
        public CourseQuestion()
        {
            this.CourseResults = new HashSet<CourseResult>();
            this.EvaluationResults = new HashSet<EvaluationResult>();
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
        public virtual ICollection<EvaluationResult> EvaluationResults { get; set; }
    }
}
