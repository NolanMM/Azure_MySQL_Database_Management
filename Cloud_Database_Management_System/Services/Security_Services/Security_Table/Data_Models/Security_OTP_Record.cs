using Cloud_Database_Management_System.Services.Security_Services.Security_Table.Data_Models;
using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Security_Services.Security_Table.Data_Models
{
    public class OTP_Record : Security_Data_Model_Abtraction
    {
        [Key]
        public int OTP_Index { get; set; }

        [Required]
        [MaxLength(16)]
        public string OTP_Code { get; set; }

        [Required]
        [MaxLength(1000)]
        public string User_ID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email_Address { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public DateTime Time_Created { get; set; }

        [MaxLength(20)]
        public string OTP_ID { get; set; }

        public override string ToString()
        {
            return $"OTP_Index: {OTP_Index}, OTP_Code: {OTP_Code}, User_ID: {User_ID}, Email_Address: {Email_Address}, Password: {Password}, Time_Created: {Time_Created}, OTP_ID: {OTP_ID}";
        }
    }
}
