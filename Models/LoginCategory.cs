namespace FaceDetection.Models
{
    public class LoginCategory
    {
        //UserName
        public string UserName { get; set; }
        //UserPassword
        public string UserPassword { get; set; }
        //UserCategory
        public UserLogin UserCategory { get; set; }
    }

    public enum UserLogin
    {
        AdminLogin = 1,
        Student = 2,
        Teacher = 3,
        Collage = 4
    }
}
