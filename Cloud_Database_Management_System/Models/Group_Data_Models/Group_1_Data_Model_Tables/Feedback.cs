using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class Feedback : Group_1_Record_Interface
    {
        [Required]
        public  int Feedback_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public  string User_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public  string Product_ID { get; set; }
        [Required(ErrorMessage = "Stars Rating is required")]
        public  decimal Stars_Rating { get; set; }

        public override string ToString()
        {
            return $"Feedback_ID: {Feedback_ID}, User_ID: {User_ID}, Product_ID: {Product_ID}, Stars_Rating: {Stars_Rating}";
        }
    }
}
