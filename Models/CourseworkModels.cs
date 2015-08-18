using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AdaptiveLearningFinal.Models
{
    /// <summary>
    /// This is a model that represents a version of the CourseResult Table
    /// </summary>
    public partial class Questions
    {
            /// <summary>
            /// Constructor for Questions class
            /// </summary>
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

            public int counter { get; set; }
            public int SelectedItem { get; set; }
            public int id { get; set; }

        
    }

    /// <summary>
    /// This is the model that represents a version of the Topic Table
    /// </summary>
    public partial class ChooseCourse
    {
        /// <summary>
        /// Constructor for ChooseCourse class
        /// </summary>
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
    /// <summary>
    /// This is the model that represents a version of the CourseResult Table
    /// </summary>
    public partial class TestResults
    {
        public int CourseResultID { get; set; }
        public Nullable<int> QuestionID { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public Nullable<bool> Correct { get; set; }

        public virtual CourseQuestion CourseQuestion { get; set; }
        public virtual User User { get; set; }
    }

    /// <summary>
    /// This is the model that represents a version of the Courses Table
    /// </summary>
    public partial class ClassModel
    {
        /// <summary>
        /// Constructor for the ClassModel class
        /// </summary>
        public ClassModel()
        {
            this.CourseMaterials = new HashSet<CourseMaterial>();
            this.CourseQuestions = new HashSet<CourseQuestion>();
        }

        public int ClassID { get; set; }
        [Display(Name="ClassName")]
        public string ClassName { get; set; }
        public Nullable<int> TopicId { get; set; }
        public Nullable<int> ClassLevel { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<CourseMaterial> CourseMaterials { get; set; }
        public virtual ICollection<CourseQuestion> CourseQuestions { get; set; }

        public string SelectedItemValue { get; set; }
        public string SelectedItem { get; set; }
    }

    /// <summary>
    /// This is a model that represents a version of the CourseMaterial Table
    /// </summary>
    public partial class LearningModel
    {
        public int MaterialID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string VideoUrl { get; set; }
        public string ReadingUrl { get; set; }

        public virtual Course Course { get; set; }

        public string SelectedItem { get; set; }
    }
   
}