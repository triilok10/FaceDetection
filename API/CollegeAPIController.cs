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
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Mode", 1);
                    cmd.Parameters.AddWithValue("@CollegeID", pCollegeDetails.CollegeID);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            obj.CollegeID = Convert.ToInt32(rdr["CollegeID"]);
                            obj.CollegeName = Convert.ToString(rdr["CollegeName"]);
                            obj.CollegeCode = Convert.ToString(rdr["CollegeCode"]);
                            obj.Status = true;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                obj.ErrMsg = ex.Message;
                obj.Status = false;
            }
            return obj;

        }
        #endregion

        #region "GET LIST CollegeUser"
        [HttpGet]
        public List<CollegeDetails> GetCollegeUser(int CollegeID)
        {
            List<CollegeDetails> lstCollegeDetails = new List<CollegeDetails>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_CollegeUser", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Mode", 3);
                        cmd.Parameters.AddWithValue("@CollegeID", CollegeID);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                lstCollegeDetails.Add(new CollegeDetails
                                {
                                    IsActive = Convert.ToBoolean(rdr["IsActive"]),
                                    Username = Convert.ToString(rdr["Username"]),
                                    Password = Convert.ToString(rdr["Password"]),
                                    CollegeUserID = Convert.ToInt32(rdr["CollegeUserID"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception("An error occurred while retrieving college users.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred.", ex);
            }

            return lstCollegeDetails;
        }
        #endregion


        #region "POST CollegeUserPost"
        [HttpPost]
        public CollegeDetails CollegeUserPost([FromBody] CollegeDetails pCollegeDetails)
        {
            CollegeDetails obj = new CollegeDetails();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {

                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_CollegeUser", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Mode", 2);
                    cmd.Parameters.AddWithValue("@CollegeID", pCollegeDetails.CollegeID);
                    cmd.Parameters.AddWithValue("@Username", pCollegeDetails.Username);
                    cmd.Parameters.AddWithValue("@Password", pCollegeDetails.Password);
                    cmd.Parameters.AddWithValue("@IsSystemUser", pCollegeDetails.IsSystemUser);
                    cmd.Parameters.AddWithValue("@IsActive", pCollegeDetails.IsActive);
                    cmd.Parameters.AddWithValue("@CreatedBy", pCollegeDetails.CreatedBy);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            obj.ErrMsg = Convert.ToString(rdr["Message"]);
                            obj.StatusCode = Convert.ToInt32(rdr["StatusCode"]);
                            obj.Status = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                obj.ErrMsg = ex.Message;
                obj.Status = false;
            }
            return obj;
        }
        #endregion
    }
}
