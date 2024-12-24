using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Reflection;

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

            int userCategoryValue = (int)Enum.Parse(typeof(UserLogin), pLoginCategory.UserCategoryString);

            LoginCategory obj = new LoginCategory();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_Login", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", 1);
                    cmd.Parameters.AddWithValue("@UserCategory", userCategoryValue);
                    cmd.Parameters.AddWithValue("@UserName", pLoginCategory.UserName);
                    cmd.Parameters.AddWithValue("@UserPassword", pLoginCategory.UserPassword);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            obj.AdminLoginID = Convert.ToInt32(rdr["AdminLoginID"]);
                            obj.AdminLoginCode = Convert.ToString(rdr["AdminLoginCode"]);
                            obj.UserName = Convert.ToString(rdr["UserName"]);
                            obj.UserPassword = Convert.ToString(rdr["Password"]);
                            obj.IsSystemUser = Convert.ToBoolean(rdr["IsSystemUser"]);
                            obj.UserCategoryString = Convert.ToString(userCategoryValue);
                            obj.IsSuccess = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                res = false;
                obj.IsSuccess = res;
            }

            return Ok(obj);
        }

    }
}
