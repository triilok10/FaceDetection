using FaceDetection.AppCode;
using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;

namespace FaceDetection.Controllers
{
    [ServiceFilter(typeof(SessionAdmin))]

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

        [HttpGet]
        public async Task<IActionResult> CollegeUser(int CollegeID)
        {
            try
            {
                CollegeDetails pCollegeDetails = new CollegeDetails();
                pCollegeDetails.CollegeID = CollegeID;
                if (pCollegeDetails.CollegeID <= 0)
                {
                    return RedirectToAction("CollegeInfo", "UserAdmin", new { errorMsg = "Please select the College" });
                }


                string apiUrl = $"{baseUrl}api/CollegeAPI/CollegeUser";
                var jsonContent = JsonConvert.SerializeObject(pCollegeDetails);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


                var getResponse = await _httpClient.PostAsync(apiUrl, content);
                if (getResponse.IsSuccessStatusCode)
                {
                    var responseBody = await getResponse.Content.ReadAsStringAsync();
                    var obj = JsonConvert.DeserializeObject<CollegeDetails>(responseBody);

                    // Get list of college users
                    string apiGetUsersUrl = $"{baseUrl}api/CollegeAPI/GetCollegeUser?CollegeID={pCollegeDetails.CollegeID}";
                    var listResponse = await _httpClient.GetAsync(apiGetUsersUrl);

                    if (listResponse.IsSuccessStatusCode)
                    {
                        var listBody = await listResponse.Content.ReadAsStringAsync();
                        List<CollegeDetails> lstCollegeUsers = JsonConvert.DeserializeObject<List<CollegeDetails>>(listBody);

                        if (lstCollegeUsers.Count > 0)
                        {
                            ViewBag.CollegeList = lstCollegeUsers;
                        }
                    }

                    if (obj.Status == true)
                    {
                        return View(obj);
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CollegeInfo", "UserAdmin", new { errorMsg = "An error occurred while processing your request." });
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> InsertCollegeUser(CollegeDetails pCollegeDetails)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(pCollegeDetails.Username) && !string.IsNullOrWhiteSpace(pCollegeDetails.Password))
                {
                    // Prepare to post the data
                    string apiUrl = $"{baseUrl}api/CollegeAPI/CollegeUserPost";
                    pCollegeDetails.CreatedBy = _clsSession.GetInt32("UserID");
                    var jsonContent = JsonConvert.SerializeObject(pCollegeDetails);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Send POST request
                    var postResponse = await _httpClient.PostAsync(apiUrl, content);
                    if (postResponse.IsSuccessStatusCode)
                    {
                        var responseBody = await postResponse.Content.ReadAsStringAsync();
                        var obj = JsonConvert.DeserializeObject<CollegeDetails>(responseBody);

                        if (obj.Status == true)
                        {
                            if (obj.StatusCode == 2)
                            {
                                TempData["errorMessage"] = obj.ErrMsg;
                            }

                            return RedirectToAction("CollegeUser", "College", new { CollegeID = pCollegeDetails.CollegeID });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("CollegeInfo", "UserAdmin", new { errorMsg = "An error occurred while processing your request." });
            }

            return RedirectToAction("CollegeUser", "College", new { CollegeID = pCollegeDetails.CollegeID });

        }

        #endregion



        #region "Edit CollegeUser GET"
        [HttpPost]
        public async Task<IActionResult> EditCollegeUser([FromBody] CollegeDetails collegeDetails)
        {
            CollegeDetails obj = new CollegeDetails();
            try
            {
                if (collegeDetails.CollegeUserID <= 0)
                {
                    obj.ErrMsg = "Please select the CollegeUser to Edit";
                    obj.Status = false;
                    return Json(new { obj });
                }

                string APIURL = baseUrl + "api/CollegeAPI/EditCollegeUser";

                string json = JsonConvert.SerializeObject(collegeDetails);
                StringContent con = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage res = await _httpClient.PostAsync(APIURL, con);
                if (res.IsSuccessStatusCode)
                {
                    dynamic resBody = await res.Content.ReadAsStringAsync();
                    obj = JsonConvert.DeserializeObject<CollegeDetails>(resBody);
                    return Json(new { obj });
                }
                else
                {
                    obj.ErrMsg = "Error in API Call";
                    obj.Status = false;
                    return Json(new { obj });
                }

            }
            catch (Exception ex)
            {
                obj.ErrMsg = ex.Message;
                obj.Status = false;
                return Json(new { obj });
            }

        }
        #endregion

    }
}
