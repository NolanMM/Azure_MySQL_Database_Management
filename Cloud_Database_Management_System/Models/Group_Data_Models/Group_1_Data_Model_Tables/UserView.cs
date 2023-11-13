using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class UserView : Group_1_Record_Interface
    {
        [Required]
        public string User_ID { get; set; }
        [Required]
        public DateOnly Start_Date { get; set; }
        [Required]
        public DateOnly End_Date { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        
        //public UserView() {
        //    User_ID = string.Empty;
        //    Timestamp = DateTime.MinValue;
        //    End_Date = new DateOnly();
        //    Start_Date = new DateOnly();
        //}
        public override string ToString()
        {
            return $"User_ID: {User_ID}, Timestamp: {Timestamp}, End_Date: {End_Date}, Start_Date: {Start_Date}";
        }
    }
}
