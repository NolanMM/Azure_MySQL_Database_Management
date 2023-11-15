using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Services.Security_Services.Security_Table.Data_Models
{
    public class Security_UserId_Record : Security_Data_Model_Abtraction
    {
        [Required]
        public int Index_UserID { get; set; }

        [Required]
        [MaxLength(1000)]
        public string User_ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email_Address { get; set; }

        public override string ToString()
        {
            return $"Index_UserID: {Index_UserID}, User_ID: {User_ID}, Email Address: {Email_Address}";
        }
    }
}
