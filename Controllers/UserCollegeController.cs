using Microsoft.AspNetCore.Mvc;

namespace FaceDetection.Controllers
{
    public class UserCollegeController : Controller
    {

        [HttpGet]
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CollegeSetting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TeacherRecord()
        {
            return View();
        }

        [HttpGet]
        public IActionResult StudentRecord()
        {
            return View();
        }

    }

}