namespace FaceDetection.Models
{
    public class CollageDetails
    {
        //CollageID
        public int CollageID { get; set; }
        //CollageName
        public string CollageName { get; set; }
        //CollageCode
        public string CollageCode { get; set; }
        //CollageCity
        public string CollageCity { get; set; }
        //CollageState
        public string CollageState { get; set; }
        //CollageCountry
        public string CollageCountry { get; set; }
        //CollagePinCode
        public string CollagePinCode { get; set; }
        //CollagePhone
        public string CollagePhone { get; set; }
        //CollageMail
        public string CollageMail { get; set; }
        //IsCollageActive
        public bool IsCollageActive { get; set; } = false; //Default False

    }
}
