using FaceDetection.AppCode;
using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FaceDetection.Controllers
{
    public class CollegeController : Controller
    {

        #region "Variables and Constructors"

        private readonly HttpClient _httpClient;
        private readonly dynamic baseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClsSession _clsSession;
        public CollegeController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IClsSession clsSession)
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

        #region "GET/POST CollegeUser"

        [HttpGet, HttpPost]
        public async Task<IActionResult> CollegeUser(CollegeDetails pCollegeDetails)
        {
            try
            {
                CollegeDetails obj = new CollegeDetails();
                //To Post the Data
                if (!string.IsNullOrWhiteSpace(pCollegeDetails.Username) && !string.IsNullOrWhiteSpace(pCollegeDetails.Password))
                {
                    string APIURL = baseUrl + "api/CollegeAPI/CollegeUserPost";

                    string Json = JsonConvert.SerializeObject(pCollegeDetails);
                    StringContent con = new StringContent(Json, Encoding.UTF8, "application/json");

                    HttpResponseMessage res = await _httpClient.PostAsync(APIURL, con);

                    if (res.IsSuccessStatusCode)
                    {
                        dynamic resBody = await res.Content.ReadAsStringAsync();
                        obj = JsonConvert.DeserializeObject<CollegeDetails>(resBody);

                        if (obj.Status == true)
                        {
                            return View(obj);
                        }
                    }

                }
                else  //To GET the Data
                {

                    if (pCollegeDetails.CollegeID <= 0)
                    {
                        obj.ErrMsg = "Please select the College";
                        obj.Status = false;
                        return RedirectToAction("CollegeInfo", "UserAdmin");
                    }

                    string APIURL = baseUrl + "api/CollegeAPI/CollegeUser";
                    string Json = JsonConvert.SerializeObject(pCollegeDetails);
                    StringContent con = new StringContent(Json, Encoding.UTF8, "application/json");

                    HttpResponseMessage res = await _httpClient.PostAsync(APIURL, con);
                    if (res.IsSuccessStatusCode)
                    {
                        dynamic resBody = await res.Content.ReadAsStringAsync();
                        obj = JsonConvert.DeserializeObject<CollegeDetails>(resBody);

                        if (obj.Status == true)
                        {
                            return View(obj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }




            return RedirectToAction("CollegeInfo", "UserAdmin");
        }

        #endregion


    }
}
