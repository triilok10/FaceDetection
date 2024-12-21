namespace FaceDetection.Models
{
    public class TeacherDetails
    {
        //TeacherID
        public string TeacherID { get; set; }
        //TeacherCode
        public string TeacherCode { get; set; }
        //TeacherName
        public string TeacherName { get; set; }
        //TeacherType   1. Principal  2. ClassTeacher 3.Others.
        public int TeacherType { get; set; }
        //TeacherCity
        public string TeacherCity { get; set; }
        //TeacherState
        public string TeacherState { get; set; }
        //TeacherCountry
        public string TeacherCountry { get; set; }
        //TeacherGender
        public string TeacherGender { get; set; }
        //TeacherMail
        public string TeacherMail { get; set; }
        //TeacherPhone
        public string TeacherPhone { get; set; }
        //IsTeacherActive
        public bool IsTeacherActive { get; set; } = false;

    }
}
