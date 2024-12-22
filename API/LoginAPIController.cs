using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FaceDetection.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginAPIController : ControllerBase
    {
        private readonly string _connectionString;

        public LoginAPIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }


        [HttpPost]
        public IActionResult LoginAuth([FromBody] LoginCategory pLoginCategory)
        {
            bool res = false;
            string message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("", 1);
                    cmd.Parameters.AddWithValue("", pLoginCategory.UserCategory);
                    cmd.Parameters.AddWithValue("", pLoginCategory.UserName);
                    cmd.Parameters.AddWithValue("", pLoginCategory.UserPassword);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read()) { }

                    }
                }
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
