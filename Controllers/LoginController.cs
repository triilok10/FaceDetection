using Microsoft.AspNetCore.Mvc;

namespace FaceDetection.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAuth()
        {
            return View();
        }
    }
}
