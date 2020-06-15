using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using lab04_2.Models;
using Microsoft.AspNet.Identity;

namespace lab04_2.Controllers.Api
{
    
    public class CourseController : ApiController
    {
        public ApplicationDbContext _db { get; set; }
        public CourseController()
        {
            _db = new ApplicationDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var useid = User.Identity.GetUserId();
            var course = _db.Courses.Single(c => c.Id == id && c.LectureId == useid);
            if (course.IsCanceled)
                return NotFound();
            course.IsCanceled = true;
            _db.SaveChanges();
            return Ok();
        }
    }
}
