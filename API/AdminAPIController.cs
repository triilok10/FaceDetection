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
                                CountryID = rdr.GetInt32(rdr.GetOrdinal("StateID")),
                                CountryName = rdr.GetString(rdr.GetOrdinal("StateName"))
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

    }
}
