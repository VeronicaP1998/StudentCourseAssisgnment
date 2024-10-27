using Microsoft.Ajax.Utilities;
using Naffco.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Naffco.DataAccessLayer
{
    public class StudentDAL
    {
        private StudentDBEntities dBEntities = new StudentDBEntities();
        public int InsertStudent(tblStudent ObjtblStudent)
        {
            int result = 0;
            try
            {
                if (ObjtblStudent.StudentID != null && ObjtblStudent.StudentID != 0)
                {
                    result = UpdateStudent(ObjtblStudent);
                }
                else
                {
                    using (var db = new StudentDBEntities())
                    {
                        result = db.Database.ExecuteSqlCommand("EXEC CreateStudent @Email, @StudentName, @City,@MobileNum",
                             new SqlParameter("@Email", ObjtblStudent.Email),
                             new SqlParameter("@StudentName", ObjtblStudent.StudentName),
                             new SqlParameter("@City", ObjtblStudent.City),
                             new SqlParameter("@MobileNum", ObjtblStudent.MobileNum));

                        if (result != 0)
                            db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }


        public int UpdateStudent(tblStudent ObjtblStudent)
        {
            int result = 0;
            try
            {
                using (var db = new StudentDBEntities())
                {
                    var existingStudent = GetStudentByID(ObjtblStudent.StudentID);
                    if (existingStudent != null)
                    {
                        result = db.Database.ExecuteSqlCommand("EXEC EditStudent @StudentID,@Email,@City,@MobileNum",
                         new SqlParameter("@StudentID", ObjtblStudent.StudentID),
                         new SqlParameter("@Email", ObjtblStudent.Email),
                         new SqlParameter("@City", ObjtblStudent.City),
                         new SqlParameter("@MobileNum", ObjtblStudent.MobileNum));
                        if (result != 0)
                            db.SaveChanges();
                    }

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }

        public List<tblStudent> GetStudentList()
        {

            List<tblStudent> getStudentLists = new List<tblStudent>();
            try
            {
                using (var db = new StudentDBEntities())
                {
                    getStudentLists = db.Set<tblStudent>()
            .SqlQuery("EXEC GetStudentList").ToList();

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return getStudentLists;
        }
        public int DeleteStudent(int StudentID)
        {
            int result = 0;
            try
            {
                using (var db = new StudentDBEntities())
                {
                    result = db.Database.ExecuteSqlCommand("EXEC DeleteStudent @StudentID",
                        new SqlParameter("@StudentID", StudentID));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }

        public tblStudent GetStudentByID(int StudentID)
        {

            tblStudent obj = new tblStudent();
            try
            {

                using (var db = new StudentDBEntities())
                {
                    obj = db.Set<tblStudent>().SqlQuery("EXEC GetStudentByID @StudentID",
                        new SqlParameter("@StudentID", StudentID)).FirstOrDefault();

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return obj;
        }

        public List<GetAllStudentCourses_Result> GetAllStudentAndCoursesDetailsList(string StudentName = "", string CourseName = "")
        {
            List<GetAllStudentCourses_Result> listObj = new List<GetAllStudentCourses_Result>();
            try
            {
                using (var db = new StudentDBEntities())
                {

                    listObj = (from a in db.GetAllStudentCourses()
                               select new GetAllStudentCourses_Result
                               {
                                   StudentID = a.StudentID,
                                   StudentName = a.StudentName,
                                   Email = a.Email,
                                   MobileNum = a.MobileNum,
                                   CourseID = a.CourseID,
                                   CourseName = a.CourseName,
                                   Description = a.Description

                               }).ToList();
                    if (listObj != null && listObj.Count() > 0)
                    {
                        if (!string.IsNullOrEmpty(StudentName) && !string.IsNullOrEmpty(CourseName))
                        {
                            listObj = listObj.Where(i => i.StudentName.ToLower().Contains(StudentName.ToLower()) && i.CourseName.ToLower().Contains(CourseName.ToLower())).ToList();
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(StudentName))
                            {
                                listObj = listObj.Where(i => i.StudentName.ToLower().Contains(StudentName.ToLower())).ToList();
                            }
                            if (!string.IsNullOrEmpty(CourseName))
                            {
                                listObj = listObj.Where(i => i.CourseName.ToLower().Contains(CourseName.ToLower())).ToList();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return listObj;
        }

        public string StudentEnrollment(EnrollStudent enroll)
        {
            string message = "";
            try
            {
                using (var db = new StudentDBEntities())
                {
                    var listdata = GetAllStudentAndCoursesDetailsList();
                    if (listdata != null || listdata.Count() > 0)
                    {
                        listdata = listdata.Where(i => i.StudentID == enroll.SelectedStudentId[0] && i.CourseID == enroll.SelectedCourseId[0]).ToList();
                        if (listdata.Count() == 1)
                        {
                            message= "Data Already Present";
                        }
                        else
                        {
                            var result = db.Database.ExecuteSqlCommand("EXEC StudentEnrollment @StudentID, @CourseID",
                           new SqlParameter("@StudentID", enroll.SelectedStudentId[0]),
                           new SqlParameter("@CourseID", enroll.SelectedCourseId[0]));
                          
                            if (result != 0)
                            {
                                db.SaveChanges();
                                message= "Data Saved Successfully";
                            }
                        }
                    }
                    else
                    {
                        var result = db.Database.ExecuteSqlCommand("EXEC StudentEnrollment @StudentID, @CourseID",
                            new SqlParameter("@StudentID", enroll.SelectedStudentId[0]),
                            new SqlParameter("@CourseID", enroll.SelectedCourseId[0]));

                        if (result != 0)
                        {
                            db.SaveChanges();
                            message= "Data Saved Successfully";
                        }
                            
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
                 message= "Error in Saving Data";
            }
            return message;
        }
    }
}