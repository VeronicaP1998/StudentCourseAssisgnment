using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Naffco.DataAccessLayer
{
    public class CourseDAL
    {
        private StudentDBEntities dBEntities = new StudentDBEntities();
        public int InsertCourse(tblCourse ObjtblCourse)
        {
            int result = 0;
            try
            {
                if (ObjtblCourse.CourseID != null && ObjtblCourse.CourseID != 0)
                {
                    result = UpdateCourse(ObjtblCourse);
                }
                else
                {
                    using (var db = new StudentDBEntities())
                    {
                        result = db.Database.ExecuteSqlCommand("EXEC CreateCourse @CourseName, @Description",
                             new SqlParameter("@CourseName", ObjtblCourse.CourseName),
                             new SqlParameter("@Description", ObjtblCourse.Description));

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
        public int UpdateCourse(tblCourse ObjtblCourse)
        {
            int result = 0;
            try
            {
                using (var db = new StudentDBEntities())
                {
                    var existingCourse = GetCourseByID(ObjtblCourse.CourseID);
                    if (existingCourse != null)
                    {
                        result = db.Database.ExecuteSqlCommand("EXEC EditCourse @CourseID,@CourseName,@Description",
                         new SqlParameter("@CourseID", ObjtblCourse.CourseID),
                         new SqlParameter("@CourseName", ObjtblCourse.CourseName),
                         new SqlParameter("@Description", ObjtblCourse.Description));
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
        public tblCourse GetCourseByID(int CourseID)
        {
            tblCourse obj = new tblCourse();
            try
            {
                using (var db = new StudentDBEntities())
                {
                    obj = db.Set<tblCourse>().SqlQuery("EXEC GetCourseByID @CourseID",
                        new SqlParameter("@CourseID", CourseID)).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return obj;
        }

        public List<tblCourse> GetCourseList()
        {

            List<tblCourse> getCoursesLists = new List<tblCourse>();
            try
            {
                using (var db = new StudentDBEntities())
                {
                    getCoursesLists = db.Set<tblCourse>()
            .SqlQuery("EXEC GetCourseList").ToList();

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return getCoursesLists;
        }
        public int DeleteCourse(int CourseID)
        {
            int result = 0;
            try
            {
                using (var db = new StudentDBEntities())
                {
                    result = db.Database.ExecuteSqlCommand("EXEC DeleteCourse @CourseID",
                        new SqlParameter("@CourseID", CourseID));
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }
    }
}