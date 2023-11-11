namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class Feedback : Group_1_Record_Interface
    {
        public  int Feedback_ID { get; set; }
        public  string User_ID { get; set; }
        public  string Product_ID { get; set; }
        public  decimal Stars_Rating { get; set; }

        public Feedback()
        {
            Feedback_ID = 0;
            User_ID = string.Empty;
            Product_ID = string.Empty;
            Stars_Rating = 0;
        }
    }
}
