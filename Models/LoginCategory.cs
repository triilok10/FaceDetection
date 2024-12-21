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
        Student = 1,
        Teacher = 2,
        Collage = 3 
    }
}
