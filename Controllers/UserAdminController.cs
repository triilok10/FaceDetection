using FaceDetection.AppCode;
using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Net.Http;

namespace FaceDetection.Controllers
{
    [ServiceFilter(typeof(SessionAdmin))]
    public class UserAdminController : Controller
    {


        #region "Constructor"

        private readonly HttpClient _httpClient;
        private readonly dynamic baseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClsSession _clsSession;
        public UserAdminController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IClsSession clsSession)
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

        public async Task<PartialViewResult> College()
        {

            string StateURL = baseUrl + "api/AdminAPI/GetStateList";
            HttpResponseMessage StateResponse = await _httpClient.GetAsync(StateURL);
            if (StateResponse.IsSuccessStatusCode)
            {
                dynamic resBody = await StateResponse.Content.ReadAsStringAsync();
                List<CollegeDetails> lst = JsonConvert.DeserializeObject<List<CollegeDetails>>(resBody);
                ViewBag.StateList = lst;
            }


            string CountryURL = baseUrl + "api/AdminAPI/GetCountryList";
            HttpResponseMessage CountryResponse = await _httpClient.GetAsync(CountryURL);
            if (CountryResponse.IsSuccessStatusCode)
            {
                dynamic resBody = await CountryResponse.Content.ReadAsStringAsync();
                List<CollegeDetails> lst = JsonConvert.DeserializeObject<List<CollegeDetails>>(resBody);
                ViewBag.CountryList = lst;
            }
            return PartialView();
        }

        [HttpPost]
        public IActionResult CollegeAdd(CollegeDetails pCollegeDetails)
        {
            try
            {

            }
            catch (Exception ex)
            {


            }
            return RedirectToAction("CollegeInfo", "UserAdmin");
        }
    }

}
