﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class AdaptiveLearningEntities : DbContext
    {
        public AdaptiveLearningEntities()
            : base("name=AdaptiveLearningEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Application> Applications { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseMaterial> CourseMaterials { get; set; }
        public DbSet<CourseQuestion> CourseQuestions { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }
        public DbSet<EvaluationResult> EvaluationResults { get; set; }
        public DbSet<LearningStyle> LearningStyles { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersInRole> UsersInRoles { get; set; }
        public DbSet<UserTopic> UserTopics { get; set; }
    }
}
