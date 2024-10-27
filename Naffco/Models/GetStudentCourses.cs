using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Naffco.Models
{
    public class GetStudentCourses
    {
            public int CourseID { get; set; }
            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public string Email { get; set; }
            public string MobileNum { get; set; }
            public string CourseName { get; set; }
            public string Description { get; set; }
    }
}