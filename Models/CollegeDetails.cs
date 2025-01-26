namespace FaceDetection.Models
{
    public class CollegeDetails
    {
        //CollegeID
        public int? CollegeID { get; set; }
        //CollegeName
        public string? CollegeName { get; set; }
        //CollegeCode
        public string? CollegeCode { get; set; }
        //CollegeCity
        public string? CollegeCity { get; set; }
        //CollegeState
        public string? CollegeState { get; set; }
        //CollegeCountry
        public string? CollegeCountry { get; set; }
        //CollegePinCode
        public string? CollegePinCode { get; set; }
        //CollegePhone
        public string? CollegePhone { get; set; }
        //CollegeMail
        public string? CollegeMail { get; set; }
        //CollegeAdmin
        public string? CollegeAdmin { get; set; }
        //CollegeWebsite
        public string? CollegeWebsite { get; set; }
        //IsCollegeActive
        public bool IsCollegeActive { get; set; }
        public string? CountryName { get; set; }
        public int? CountryID { get; set; }
        public string? StateName { get; set; }
        public int? StateID { get; set; }
        public bool Status { get; set; }
        public int? StatusCode { get; set; }
        public string? ErrMsg { get; set; }
        public bool IsUpdate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }

        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool IsSystemUser { get; set; }
        public bool IsActive { get; set; }
        public string? PDFHtml { get; set; } 
    }
}
