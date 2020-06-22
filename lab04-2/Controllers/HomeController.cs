using lab04_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using lab04_2.ViewModels;

namespace lab04_2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upCourse = _dbContext.Courses
                .Include(c => c.Lecture)
                .Include(c => c.Category)
                .Where(c => c.DateTime < DateTime.Now);
            var viewModel = new CoursesViewModel
            {
                UpCourses = upCourse,
                ShowAction = User.Identity.IsAuthenticated
            };
            //var viewModel = from t in _dbContext.Courses
            //                select t;
            return View(viewModel);
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}