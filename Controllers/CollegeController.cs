using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;

namespace FaceDetection.Controllers
{
    public class CollegeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CollegeUser(CollegeDetails pCollegeDetails)
        {
            return View();
        }
    }
}
