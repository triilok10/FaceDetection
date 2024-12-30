using FaceDetection.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FaceDetection.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminAPIController : ControllerBase
    {

        private readonly string _connectionString;

        public AdminAPIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }

        #region "Get CountryList"
        [HttpGet]
        public IActionResult GetCountryList()
        {
            bool res = false;

            List<CollegeDetails> countryList = new List<CollegeDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_MasterData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ViewMode", 1);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            countryList.Add(new CollegeDetails
                            {
                                CountryID = rdr.GetInt32(rdr.GetOrdinal("CountryID")),
                                CountryName = rdr.GetString(rdr.GetOrdinal("CountryName"))
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, ErrMsg = ex.Message });
            }
            return Ok(countryList);
        }
        #endregion

        #region "Get StateList"
        [HttpGet]
        public IActionResult GetStateList()
        {
            List<CollegeDetails> stateList = new List<CollegeDetails>();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("usp_MasterData", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ViewMode", 2);

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                stateList.Add(new CollegeDetails
                                {
                                    StateID = rdr.GetInt32(rdr.GetOrdinal("StateID")),
                                    StateName = rdr.GetString(rdr.GetOrdinal("StateName"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, ErrMsg = ex.Message });
            }
            return Ok(stateList);
        }
        #endregion

        #region "Insert Collage"
        public IActionResult InsertCollege([FromBody] CollegeDetails pCollegeDetails)
        {
            CollegeDetails obj = new CollegeDetails();

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_College", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@Mode", 1);
                    cmd.Parameters.AddWithValue("@CollegeName", pCollegeDetails.CollegeName);
                    cmd.Parameters.AddWithValue("@CollegeCode", pCollegeDetails.CollegeCode);
                    cmd.Parameters.AddWithValue("@CollegeCity", pCollegeDetails.CollegeCity);
                    cmd.Parameters.AddWithValue("@CollegeStateID", pCollegeDetails.StateID);
                    cmd.Parameters.AddWithValue("@CollegeCountryID", pCollegeDetails.CountryID);
                    cmd.Parameters.AddWithValue("@CollegePinCode", pCollegeDetails.CollegePinCode);
                    cmd.Parameters.AddWithValue("@CollegeAdmin", pCollegeDetails.CollegeAdmin);
                    cmd.Parameters.AddWithValue("@CollegeMail", pCollegeDetails.CollegeMail);
                    cmd.Parameters.AddWithValue("@CollegePhone", pCollegeDetails.CollegePhone);
                    cmd.Parameters.AddWithValue("@CollegeWebsite", pCollegeDetails.CollegeWebsite);
                    cmd.Parameters.AddWithValue("@IsCollegeActive", pCollegeDetails.IsCollegeActive);
                    cmd.ExecuteNonQuery();
                }
                obj.Status = true;
                obj.ErrMsg = "Data inserted successfully.";
            }
            catch (Exception ex)
            {
                obj.Status = false;
                obj.ErrMsg = ex.Message;
            }
            return Ok(obj);
        }
        #endregion

        #region "GetCollegeList"
        [HttpGet]
        public IActionResult GetCollegeList()
        {
            List<CollegeDetails> lstCollege = new List<CollegeDetails>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("usp_College", con);
                    cmd.Parameters.AddWithValue("@Mode", 2);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            lstCollege.Add(new CollegeDetails
                            {
                                CollegeID = Convert.ToInt32(rdr["CollegeID"]),
                                CollegeName = Convert.ToString(rdr["CollegeName"]),
                                CollegeCode = Convert.ToString(rdr["CollegeCode"]),
                                CollegeCity = Convert.ToString(rdr["CollegeCity"]),
                                CreatedOn = Convert.ToDateTime(rdr["CreateDate"]),
                                CollegeAdmin = Convert.ToString(rdr["CollegeAdmin"]),
                                IsCollegeActive = Convert.ToBoolean(rdr["IsCollegeActive"]),
                            });
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                return BadRequest(400);
            }
            return Ok(lstCollege);
        }

        #endregion
    }
}
