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
    
    public partial class User
    {
        public User()
        {
            this.CourseResults = new HashSet<CourseResult>();
            this.EvaluationResults = new HashSet<EvaluationResult>();
            this.LearningStyles = new HashSet<LearningStyle>();
            this.UsersInRoles = new HashSet<UsersInRole>();
        }
    
        public System.Guid ApplicationId { get; set; }
        public System.Guid UserId { get; set; }
        public string UserName { get; set; }
        public bool IsAnonymous { get; set; }
        public System.DateTime LastActivityDate { get; set; }
    
        public virtual Application Application { get; set; }
        public virtual ICollection<CourseResult> CourseResults { get; set; }
        public virtual ICollection<EvaluationResult> EvaluationResults { get; set; }
        public virtual ICollection<LearningStyle> LearningStyles { get; set; }
        public virtual Membership Membership { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual ICollection<UsersInRole> UsersInRoles { get; set; }
    }
}