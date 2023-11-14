using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class UserView : Group_1_Record_Interface
    {
        public int UserView_ID { get; set; }
        [Required]
        public string User_ID { get; set; }
        [Required]
        public string Product_ID { get; set; }
        [Required]
        public decimal Time_Count { get; set; }
        [Required]
        public DateTime Date_Access { get; set; }
        
        //public UserView() {
        //    User_ID = string.Empty;
        //    Timestamp = DateTime.MinValue;
        //    End_Date = new DateOnly();
        //    Start_Date = new DateOnly();
        //}
        public override string ToString()
        {
            return $"UserView_ID: {UserView_ID}, User_ID: {User_ID}, Product_ID: {Product_ID}, Time_Count: {Time_Count}, Date_Access: {Date_Access}";
        }
    }
}
