﻿using FaceDetection.AppCode;
using FaceDetection.Models;
using Microsoft.AspNetCore.Mvc;
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
            return View();
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
                string Json = JsonSerializer.Serialize(pLoginCategory);
                StringContent con = new StringContent(Json, Encoding.UTF8, "application/json");

                HttpResponseMessage apiResponse = await _httpClient.PostAsync(apiUrl, con);
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("", "");
        }

        #endregion
    }
}
