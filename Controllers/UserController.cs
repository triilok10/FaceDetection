using FaceDetection.AppCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FaceDetection.Controllers
{
    [ServiceFilter(typeof(SessionAdmin))]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserAdmin()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserCollege()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserTeacher()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserStudent()
        {
            return View();
        }
    }

    public class SessionAdmin : ActionFilterAttribute
    {
        private readonly IClsSession _clsSession;

        public SessionAdmin(IClsSession clsSession)
        {
            _clsSession = clsSession;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            int? UserId = _clsSession.GetInt32("UserID");
            string Username = _clsSession.GetString("UserName");
            string LoginType = _clsSession.GetString("LoginType");
            if (string.IsNullOrEmpty(Username) || UserId == null || string.IsNullOrWhiteSpace(LoginType))
            {
                _clsSession.SetString("LoginAuth", "Auth");
                context.Result = new RedirectToActionResult("Login", "Login", null);
            }
            base.OnActionExecuting(context);
        }
    }
}
