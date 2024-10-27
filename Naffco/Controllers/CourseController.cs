using Naffco.DataAccessLayer;
using Naffco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Naffco.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course
        public ActionResult Index()
        {
            return RedirectToAction("GetCourseList");
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblCourse ObjtblCourse)
        {

           
            CourseDAL courseDAL = new CourseDAL();
            var response = courseDAL.InsertCourse(ObjtblCourse);
            if (response != 0)
            {
                TempData["result1"] = "Course Saved Successfully";
                ModelState.Clear(); // clearing model
                return RedirectToAction("GetCourseList");
            }
            else
            {
                ModelState.AddModelError("", "Error in Saving Data");
                return View(ObjtblCourse);
            }
          
        }
        public ActionResult GetCourseList()
        {
            CourseDAL courseDAL = new CourseDAL();
            var response = courseDAL.GetCourseList();
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
        public ActionResult Edit(int CourseID)
        {
            CourseDAL courseDAL = new CourseDAL();
            var response = courseDAL.GetCourseByID(CourseID);
            if (response != null)
            {
                tblCourse Objcourse = new tblCourse();
                Objcourse.CourseID = response.CourseID;
                Objcourse.CourseName = response.CourseName;
                Objcourse.Description = response.Description;

                return View("Create", Objcourse);
            }
            else
            {
                ModelState.AddModelError("", "Could Not Find the Course Details");
                return RedirectToAction("GetCourseList");
            }
        }
        [HttpPost]
        public ActionResult Edit(tblCourse tblCourse)
        {
            if (ModelState.IsValid)
            {
                CourseDAL courseDAL = new CourseDAL();
                var response = courseDAL.UpdateCourse(tblCourse);
                if (response != 0)
                {
                    TempData["EditCourse"] = "Course Updated Successfully";
                    ModelState.Clear(); // clearing model
                    return RedirectToAction("GetCourseList");
                }
                else
                {
                    ModelState.AddModelError("", "Error in Updating Data");
                    return View(tblCourse);
                }
            }
            else
            {
                ModelState.AddModelError("", "Make sure all the data is entered");
                return View();
            }
        }

        public ActionResult Delete(int CourseID)
        {
            CourseDAL courseDAL = new CourseDAL();
            var response = courseDAL.DeleteCourse(CourseID);
            if (response != null)
            {
                TempData["DeleteStudent"] = "Student Deleted Successfully";
                ModelState.Clear(); // clearing model
                return RedirectToAction("GetCourseList", response);
            }
            else
            {
                ModelState.AddModelError("", "Could Not Find the Student Details");
                return RedirectToAction("GetCourseList");
            }
        }

    }
}
