using Naffco.DataAccessLayer;
using Naffco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace Naffco.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return RedirectToAction("GetStudentList");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblStudent ObjtblStudent)
        {

            if (!string.IsNullOrEmpty(ObjtblStudent.StudentName) && !string.IsNullOrEmpty(ObjtblStudent.Email) && !string.IsNullOrEmpty(ObjtblStudent.City) && !string.IsNullOrEmpty(ObjtblStudent.MobileNum))
            {
                StudentDAL studentDAL = new StudentDAL();
                var response = studentDAL.InsertStudent(ObjtblStudent);
                if (response != 0)
                {
                    TempData["result1"] = "Student Saved Successfully";
                    ModelState.Clear(); // clearing model
                    return RedirectToAction("GetStudentList");
                }
                else
                {
                    ModelState.AddModelError("", "Error in Saving Data");
                    return View(ObjtblStudent);
                }
            }
            else
            {
                ModelState.AddModelError("", "Make sure all the data is entered");
                return View();
            }
        }
        public ActionResult GetStudentList()
        {
            StudentDAL studentDAL = new StudentDAL();
            var response = studentDAL.GetStudentList();
            if (response != null && response.Count() > 0)
            {
                return View(response);
            }
            else
            {
                ModelState.AddModelError("", "No Data Found");
                return View();
            }
        }
        [HttpGet]
        public ActionResult Edit(int StudentID)
        {
            StudentDAL studentDAL = new StudentDAL();
            var response = studentDAL.GetStudentByID(StudentID);
            if (response != null)
            {
                tblStudent Objstudent = new tblStudent();
                Objstudent.StudentID = response.StudentID;
                Objstudent.Email = response.Email;
                Objstudent.City = response.City;
                Objstudent.MobileNum = response.MobileNum;
                Objstudent.StudentName = response.StudentName;

                return View("Create", Objstudent);
            }
            else
            {
                ModelState.AddModelError("", "Could Not Find the Student Details");
                return RedirectToAction("GetStudentList");
            }
        }
        [HttpPost]
        public ActionResult Edit(tblStudent tblStudent)
        {
            if (ModelState.IsValid)
            {
                StudentDAL studentDAL = new StudentDAL();
                var response = studentDAL.UpdateStudent(tblStudent);
                if (response != 0)
                {
                    TempData["EditStudent"] = "Student Updated Successfully";
                    ModelState.Clear(); // clearing model
                    return RedirectToAction("GetStudentList");
                }
                else
                {
                    ModelState.AddModelError("", "Error in Updating Data");
                    return View(tblStudent);
                }
            }
            else
            {
                ModelState.AddModelError("", "Make sure all the data is entered");
                return View();
            }
        }

        public ActionResult Delete(int StudentID)
        {
            StudentDAL studentDAL = new StudentDAL();
            var response = studentDAL.DeleteStudent(StudentID);
            if (response != null)
            {
                TempData["DeleteStudent"] = "Student Deleted Successfully";
                ModelState.Clear(); // clearing model
                return RedirectToAction("GetStudentList", response);
            }
            else
            {
                ModelState.AddModelError("", "Could Not Find the Student Details");
                return RedirectToAction("GetStudentList");
            }
        }

        [HttpGet]
        public ActionResult GetStudentAndCourseDetailsList()
        {
            if (ModelState.IsValid)
            {
                StudentDAL studentDAL = new StudentDAL();
                var response = studentDAL.GetAllStudentAndCoursesDetailsList();
                if (response != null && response.Count() > 0)
                {
                    return View(response);
                }
                return View(response);
            }
            else
            {
                ModelState.AddModelError("", "No Data Found");
                return View();
            }

        }
        [HttpPost]
        public ActionResult GetStudentAndCourseDetailsList(List<GetAllStudentCourses_Result> ObjListData)
        {
            try
            {

                return View(ObjListData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public ActionResult GetSearchResults(string StudentName, string CourseName)
        {
            StudentDAL studentDAL = new StudentDAL();
            var response = studentDAL.GetAllStudentAndCoursesDetailsList(StudentName, CourseName);
            if (response != null && response.Count() > 0)
            {
                return View("GetStudentAndCourseDetailsList", response);
            }
            else
            {
                ModelState.AddModelError("", "No Data Found");
                return View("GetStudentAndCourseDetailsList", response);
            }

        }
        [HttpGet]
        public ActionResult EnrollStudent()
        {
            StudentDAL studentDAL = new StudentDAL();
            CourseDAL courseDAL = new CourseDAL();
            EnrollStudent ObjStudentCourses = new EnrollStudent();
            var studentlist = studentDAL.GetStudentList();
            var courselist = courseDAL.GetCourseList();
            if (studentlist != null && studentlist.Count() > 0)
            {

                ObjStudentCourses.StudentList = studentlist.Select(a => new SelectListItem
                {
                    Text = a.StudentName,
                    Value = a.StudentID.ToString()
                }).ToList();

            }

            if (courselist != null && courselist.Count() > 0)
            {

                ObjStudentCourses.CourseList = courselist.Select(a => new SelectListItem
                {
                    Text = a.CourseName,
                    Value = a.CourseID.ToString()
                }).ToList();
            }
            return View(ObjStudentCourses);
        }
        [HttpPost]
        public ActionResult EnrollStudent(EnrollStudent enrollStudent)
        {
            StudentDAL studentDAL = new StudentDAL();
            var response = studentDAL.StudentEnrollment(enrollStudent);
            if (response != null)
            {
                TempData["EnrollStudent"] = response;
                ModelState.Clear(); // clearing model

            }
            return RedirectToAction("GetStudentAndCourseDetailsList");

        }
    }
}
