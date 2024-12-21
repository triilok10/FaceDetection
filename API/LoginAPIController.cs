using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaceDetection.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {

        [HttpPost]
        public IActionResult LoginAuth([FromBody] LoginCategory pLoginCategory)
        {
            bool res = false;
            string message = string.Empty;
            try
            {

            }
            catch (Exception ex)
            {
                message = ex.Message;
                res = false;
            }

            return Ok(new { });
        }

    }
}
