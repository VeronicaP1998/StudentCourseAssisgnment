﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Naffco
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class StudentDBEntities : DbContext
    {
        public StudentDBEntities()
            : base("name=StudentDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCourse> tblCourses { get; set; }
        public virtual DbSet<tblStdCours> tblStdCourses { get; set; }
        public virtual DbSet<tblStudent> tblStudents { get; set; }
    
        public virtual ObjectResult<CheckForExistingStudentEnrollment_Result> CheckForExistingStudentEnrollment(Nullable<int> studentID, Nullable<int> courseID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<CheckForExistingStudentEnrollment_Result>("CheckForExistingStudentEnrollment", studentIDParameter, courseIDParameter);
        }
    
        public virtual int CreateCourse(string courseName, string description)
        {
            var courseNameParameter = courseName != null ?
                new ObjectParameter("CourseName", courseName) :
                new ObjectParameter("CourseName", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateCourse", courseNameParameter, descriptionParameter);
        }
    
        public virtual int CreateStudent(string email, string studentName, string city, string mobileNum)
        {
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var studentNameParameter = studentName != null ?
                new ObjectParameter("StudentName", studentName) :
                new ObjectParameter("StudentName", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var mobileNumParameter = mobileNum != null ?
                new ObjectParameter("MobileNum", mobileNum) :
                new ObjectParameter("MobileNum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("CreateStudent", emailParameter, studentNameParameter, cityParameter, mobileNumParameter);
        }
    
        public virtual int DeleteCourse(Nullable<int> courseID)
        {
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteCourse", courseIDParameter);
        }
    
        public virtual int DeleteStudent(Nullable<int> studentID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteStudent", studentIDParameter);
        }
    
        public virtual int EditCourse(Nullable<int> courseID, string courseName, string description)
        {
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            var courseNameParameter = courseName != null ?
                new ObjectParameter("CourseName", courseName) :
                new ObjectParameter("CourseName", typeof(string));
    
            var descriptionParameter = description != null ?
                new ObjectParameter("Description", description) :
                new ObjectParameter("Description", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditCourse", courseIDParameter, courseNameParameter, descriptionParameter);
        }
    
        public virtual int EditStudent(Nullable<int> studentID, string email, string city, string mobileNum)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var cityParameter = city != null ?
                new ObjectParameter("City", city) :
                new ObjectParameter("City", typeof(string));
    
            var mobileNumParameter = mobileNum != null ?
                new ObjectParameter("MobileNum", mobileNum) :
                new ObjectParameter("MobileNum", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("EditStudent", studentIDParameter, emailParameter, cityParameter, mobileNumParameter);
        }
    
        public virtual ObjectResult<GetEnrolledStudents_Result> GetEnrolledStudents(Nullable<int> courseID)
        {
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetEnrolledStudents_Result>("GetEnrolledStudents", courseIDParameter);
        }
    
        public virtual ObjectResult<GetStudentCourses_Result> GetStudentCourses(Nullable<int> studentID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStudentCourses_Result>("GetStudentCourses", studentIDParameter);
        }
    
        public virtual int StudentEnrollment(Nullable<int> studentID, Nullable<int> courseID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("StudentEnrollment", studentIDParameter, courseIDParameter);
        }
    
        public virtual ObjectResult<GetCourseList_Result> GetCourseList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCourseList_Result>("GetCourseList");
        }
    
        public virtual ObjectResult<GetStudentList_Result> GetStudentList()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStudentList_Result>("GetStudentList");
        }
    
        public virtual ObjectResult<GetCourseByID_Result> GetCourseByID(Nullable<int> courseID)
        {
            var courseIDParameter = courseID.HasValue ?
                new ObjectParameter("CourseID", courseID) :
                new ObjectParameter("CourseID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetCourseByID_Result>("GetCourseByID", courseIDParameter);
        }
    
        public virtual ObjectResult<GetStudentByID_Result> GetStudentByID(Nullable<int> studentID)
        {
            var studentIDParameter = studentID.HasValue ?
                new ObjectParameter("StudentID", studentID) :
                new ObjectParameter("StudentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetStudentByID_Result>("GetStudentByID", studentIDParameter);
        }
    
        public virtual ObjectResult<GetAllStudentCourses_Result> GetAllStudentCourses()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetAllStudentCourses_Result>("GetAllStudentCourses");
        }
    }
}
