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
    
    public partial class EvaluationResult
    {
        public int ERID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<int> EvalQuestionID { get; set; }
        public Nullable<bool> Correct { get; set; }
    
        public virtual EvaluationTest EvaluationTest { get; set; }
        public virtual User User { get; set; }
    }
}
