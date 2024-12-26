using Microsoft.AspNetCore.Mvc;

namespace FaceDetection.Controllers
{
    public class UserAdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DashBoard()
        {
            return View();
        }

        public IActionResult CollegeInfo()
        {
            return View();
        }
        public IActionResult Module()
        {
            return View();
        }

        public PartialViewResult College()
        {
            return PartialView();
        }
    }
}
