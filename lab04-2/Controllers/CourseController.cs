using lab04_2.Models;
using lab04_2.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab04_2.Controllers
{
    
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CourseController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Course
        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var viewModel = new CourseView
            {
                Categories = _dbContext.Categories.ToList(),
                Heading = "Add Course"
            };
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseView viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course
            {
                LectureId = User.Identity.GetUserId(),  
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecture)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CoursesViewModel
            {
                UpCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses
                .Where(c => c.LectureId == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecture)
                .Include(c => c.Category)
                .ToList();
            return View(courses);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var coures = _dbContext.Courses.Single(c => c.Id == id && c.LectureId == userId);
            var viewModel = new CourseView
            {
                Categories = _dbContext.Categories.ToList(),
                Time = coures.DateTime.ToString("dd/MM/yyyy"),
                Date = coures.DateTime.ToString("HH:mm"),
                Category = coures.CategoryId,
                Place = coures.Place,
                Heading = "Edit Course",
                Id = coures.Id
            };
                
            return View("Create", viewModel);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseView coursesView)
        {
            if (!ModelState.IsValid)
            {
                coursesView.Categories = _dbContext.Categories.ToList();
                return View("Create", coursesView);
            }
            var userId = User.Identity.GetUserId();
            var coures = _dbContext.Courses.Single(c => c.Id == coursesView.Id && c.LectureId == userId);
            coures.Place = coursesView.Place;
            coures.DateTime = coursesView.GetDateTime();
            coures.CategoryId = coursesView.Category;
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        //[Authorize]
        //public ActionResult Following()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var courses = _dbContext.Followings
        //        .Where(a => a.FolloweeId == userId)
        //        .Select(a => a.Ap)
        //        .Include(l => l.Lecture)
        //        .Include(l => l.Category)
        //        .ToList();
        //    var viewModel = new CoursesViewModel
        //    {
        //        UpCourses = courses,
        //        ShowAction = User.Identity.IsAuthenticated
        //    };
        //    return View(viewModel);
        //}
    }
}