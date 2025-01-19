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

        [HttpGet]
        public async Task<IActionResult> CollegeUser(CollegeDetails pCollegeDetails)
        {
            CollegeDetails obj = new CollegeDetails();
            if (pCollegeDetails.CollegeID <= 0)
            {
                obj.ErrMsg = "Please select the College";
                obj.Status = false;
                return RedirectToAction("", "");
            }


            string APIURL = baseUrl + "api/CollegeAPI/CollegeUser";
            string Json = JsonConvert.SerializeObject(pCollegeDetails);
            StringContent con = new StringContent(Json, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await _httpClient.PostAsync(APIURL, con);
            if (res.IsSuccessStatusCode)
            {
                dynamic resBody = await res.Content.ReadAsStringAsync();
                obj = JsonConvert.DeserializeObject<CollegeDetails>(resBody);

            }

            return View();
        }
    }
}
