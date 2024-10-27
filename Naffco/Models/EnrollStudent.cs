using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Naffco.Models
{
    public class EnrollStudent
    {      
        public List<SelectListItem> CourseList { get; set; }
        public List<int> SelectedCourseId { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<int> SelectedStudentId { get; set; }
    }
}