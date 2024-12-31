using FaceDetection.AppCode;
using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

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

        #region "CollegeInfo"
        public async Task<IActionResult> CollegeInfo()
        {
            try
            {

                List<CollegeDetails> lst = new List<CollegeDetails>();
                string apiUrl = baseUrl + "api/AdminAPI/GetCollegeList";
                HttpResponseMessage apiResponse = await _httpClient.GetAsync(apiUrl);
                if (apiResponse.IsSuccessStatusCode)
                {
                    dynamic resBody = await apiResponse.Content.ReadAsStringAsync();
                    lst = JsonConvert.DeserializeObject<List<CollegeDetails>>(resBody);
                }
                return View(lst);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home", ex.Message);
            }
        }
        #endregion

        #region "College"
        [HttpPost]
        public async Task<PartialViewResult> College([FromBody] CollegeDetails pCollegeDetails)
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

            if (pCollegeDetails.CollegeID == 0)
            {

                return PartialView();
            }
            else
            {
                CollegeDetails obj = new CollegeDetails();
                string apiURL = baseUrl + $"api/AdminAPI/GetRecord?CollegeID={pCollegeDetails.CollegeID}";
                HttpResponseMessage getResponse = await _httpClient.GetAsync(apiURL);
                if (getResponse.IsSuccessStatusCode)
                {
                    dynamic resGetBody = await getResponse.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<CollegeDetails>(resGetBody);

                }
                return PartialView(obj);
            }
        }
        #endregion

        public IActionResult Module()
        {
            return View();
        }




        [HttpPost]
        public async Task<IActionResult> CollegeAdd(CollegeDetails pCollegeDetails)
        {
            CollegeDetails obj = new CollegeDetails();
            try
            {
                if (pCollegeDetails == null)
                {
                    obj.Status = false;
                    obj.ErrMsg = "Please pass the required Parameter's.";
                    return View(obj);
                }

                string url = baseUrl + "api/AdminAPI/InsertCollege";

                string json = JsonConvert.SerializeObject(pCollegeDetails);
                StringContent con = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PostAsync(url, con);
                if (res.IsSuccessStatusCode)
                {
                    dynamic resBody = await res.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<CollegeDetails>(resBody);
                }

                if (obj.Status == true)
                {
                    TempData["successMessage"] = obj.ErrMsg;
                    return RedirectToAction("CollegeInfo", "UserAdmin");
                }
                else
                {
                    TempData["errorMessage"] = obj.ErrMsg;
                    return RedirectToAction("CollegeInfo", "UserAdmin");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("CollegeInfo", "UserAdmin");
            }

        }
    }

}
