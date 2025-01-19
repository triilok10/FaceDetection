using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FaceDetection.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CollegeAPIController : ControllerBase
    {

        #region "Connection String"
        private readonly string _connectionString;

        public CollegeAPIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }
        #endregion

        #region "GetRecord CollegeUser"
        [HttpPost]
        public CollegeDetails CollegeUser([FromBody] CollegeDetails pCollegeDetails)
        {
            CollegeDetails obj = new CollegeDetails();

            try
            {
                #region "Validations"
                if (pCollegeDetails.CollegeID <= 0)
                {
                    obj.ErrMsg = "Please select the College.";
                    obj.Status = false;
                    return obj;
                }
                #endregion

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_CollegeUser", con);



                }
            }
            catch (Exception ex) { }



            return obj;

        }
        #endregion


    }
}
