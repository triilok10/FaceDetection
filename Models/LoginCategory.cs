namespace FaceDetection.Models
{
    public class LoginCategory
    {
        //UserName
        public string? UserName { get; set; }
        //UserPassword
        public string? UserPassword { get; set; }
        //UserCategory
        public UserLogin? UserCategory { get; set; }
        //UserCategoryString
        public string? UserCategoryString { get; set; }
        //IsSystemUser
        public bool? IsSystemUser { get; set; }
        //AdminLoginCode
        public string? AdminLoginCode { get; set; }
        //AdminLoginID
        public int? AdminLoginID { get; set; } = 0;
        public bool? IsSuccess { get; set; } = false;
        //CollegeCode 
        public string? CollegeCode { get; set; } = "";
        //CollegeID
        public int? CollegeID { get; set; } = 0;
    }

    public enum UserLogin
    {
        AdminLogin = 1,
        College = 2,
        Teacher = 3,
        Student = 4
    }
}
