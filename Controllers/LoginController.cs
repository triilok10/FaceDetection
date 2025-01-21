using FaceDetection.AppCode;
using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace FaceDetection.Controllers
{
    public class LoginController : Controller
    {
        #region "Constructor"

        private readonly HttpClient _httpClient;
        private readonly dynamic baseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClsSession _clsSession;
        public LoginController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IClsSession clsSession)
        {
            _httpClient = httpClient;
            _clsSession = clsSession;
            _httpContextAccessor = httpContextAccessor;
            var request = _httpContextAccessor.HttpContext.Request;
            baseUrl = $"{request.Scheme}://{request.Host.Value}/";
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        #endregion


        public IActionResult Index()
        {
            return View();
        }

        #region "Login"

        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginCategory();


            ViewBag.UserCategories = new SelectList(Enum.GetValues(typeof(UserLogin))
                .Cast<UserLogin>()
                .Select(e => new
                {
                    Value = (int)e,
                    Text = e.ToString()
                }), "Value", "Text");

            return View(model);
        }

        #endregion

        #region "Login Auth"

        [HttpPost]
        public async Task<IActionResult> LoginAuth(LoginCategory pLoginCategory)
        {
            bool res = false;
            string message = string.Empty;
            try
            {
                string apiUrl = baseUrl + "api/LoginAPI/LoginAuth";
                string Json = JsonConvert.SerializeObject(pLoginCategory);
                StringContent con = new StringContent(Json, Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = await _httpClient.PostAsync(apiUrl, con);
                if (apiResponse.IsSuccessStatusCode)
                {
                    dynamic resBody = await apiResponse.Content.ReadAsStringAsync();
                    LoginCategory obj = JsonConvert.DeserializeObject<LoginCategory>(resBody);

                    if (obj.IsSuccess == true)
                    {
                        int UserID = 0;
                        UserID = (int)obj.AdminLoginID;

                        int CollegeID = 0;
                        CollegeID = (int)obj.CollegeID;

                        string CollegeCode = "";
                        CollegeCode = (string)obj.CollegeCode;


                        string Username = (string)obj.UserName;
                        int LoginType = Convert.ToInt32(obj.UserCategoryString);

                        _clsSession.SetInt32("CollegeID", CollegeID);
                        _clsSession.SetInt32("UserID", UserID);
                        _clsSession.SetString("UserName", Username);
                        _clsSession.SetString("CollegeCode", CollegeCode);
                        _clsSession.SetInt32("LoginType", LoginType);

                        if (LoginType == 1)
                        {
                            return RedirectToAction("UserAdmin", "User");
                        }
                        if (LoginType == 2)
                        {
                            return RedirectToAction("UserCollege", "User");
                        }
                        if (LoginType == 3)
                        {
                            return RedirectToAction("UserTeacher", "User");
                        }
                        if (LoginType == 4)
                        {
                            return RedirectToAction("UserStudent", "User");
                        }
                    }
                    else
                    {
                        TempData["errorMessage"] = "Invalid Username or Password.";
                        return RedirectToAction("Login", "Login");
                    }
                }
            }
            catch (Exception ex)
            {
                res = false;
                message = ex.Message;
            }

            return RedirectToAction("Login", "Login");
        }

        #endregion
    }
}
