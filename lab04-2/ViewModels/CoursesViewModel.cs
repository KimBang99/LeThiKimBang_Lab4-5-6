using lab04_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace lab04_2.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<Course> UpCourses { get; set; }
        public bool ShowAction { get; set; }
    }
}