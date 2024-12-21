namespace FaceDetection.Models
{
    public class StudentDetails
    {
        //StudentID
        public int StudentId { get; set; }
        //StudentCode
        public string StudentCode { get; set; }
        //StudentName
        public string StudentName { get; set; }
        //StudentAddress1
        public string StudentAddress1 { get; set; }
        //StudentAddress2
        public string StudentAddress2 { get; set; }
        //StudentCity
        public string StudentCity { get; set; }
        //StudentPassword
        public string StudentPassword { get; set; }
        //StudentState
        public string StudentState { get; set; }
        //StudentPinCode
        public string StudentPinCode { get; set; }
        //StudentCountry
        public string StudentCountry { get; set; }  //Default India

        //StudentGender
        public int StudentGender { get; set; }
        //StudentPhone
        public string StudentPhone { get; set; }
        //StudentMail
        public string StudentMail { get; set; }

        public string StudentClass { get; set; }
    }
}