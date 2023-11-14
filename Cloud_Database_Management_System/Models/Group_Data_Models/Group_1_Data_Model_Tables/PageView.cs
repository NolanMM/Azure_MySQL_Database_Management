using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Group_Data_Models.Group_1_Data_Model_Tables
{
    public class PageView : Group_1_Record_Interface
    {
        public int PageView_ID { get; set; }
        [Required]
        [MaxLength(45)]
        public string Page_Name { get; set; }
        [Required]
        [MaxLength(45)]
        public string Page_Info { get; set; }
        [Required]
        [MaxLength(45)]
        public string Product_ID { get; set; }
        [Required]
        public DateTime Start_Time { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserID { get; set; }

        public override string ToString()
        {
            return $"PageView_ID: {PageView_ID}, UserID: {UserID}, Page_Name: {Page_Name}, " +
                   $"Page_Info: {Page_Info}, Product_ID: {Product_ID}, Start_Time: {Start_Time}";
        }
    }
}
