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
    
    public partial class EvaluationTest
    {
        public EvaluationTest()
        {
            this.EvaluationResults = new HashSet<EvaluationResult>();
        }
    
        public int EvalQuestionID { get; set; }
        public string EvalQuestion { get; set; }
        public string EvalAnswerA { get; set; }
        public string EvalAnswerB { get; set; }
        public string EvalAnswerC { get; set; }
        public string EvalAnswerD { get; set; }
        public string EvalCorrectAnswer { get; set; }
    
        public virtual ICollection<EvaluationResult> EvaluationResults { get; set; }
    }
}
