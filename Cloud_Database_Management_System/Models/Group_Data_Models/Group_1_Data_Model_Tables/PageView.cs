namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class PageView : Group_1_Record_Interface
    {
        public string SessionId { get; set; }
        public string Page_Name { get; set; }
        public string Page_Info { get; set; }
        public string Product_ID { get; set; }
        public DateTime Start_Time { get; set; }
        public DateTime End_Time { get; set; }
        public string UserID { get; set; }

        public PageView() {
            SessionId = string.Empty;
            UserID = string.Empty;
            Page_Name = string.Empty;
            Page_Info = string.Empty;
            Product_ID = string.Empty;
            Start_Time = DateTime.MinValue;
            End_Time = DateTime.MinValue;
        }
        public override string ToString()
        {
            return $"SessionId: {SessionId}, UserID: {UserID}, Page_Name: {Page_Name}, " +
                   $"Page_Info: {Page_Info}, Product_ID: {Product_ID}, Start_Time: {Start_Time}, End_Time: {End_Time}";
        }
    }
}
