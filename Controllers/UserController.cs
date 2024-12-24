using Microsoft.AspNetCore.Mvc;

namespace FaceDetection.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserHome()
        {
            return View();
        }
    }
}
