using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Services.Security_Services.Security_Table.Data_Models
{
    public class Security_UserId_Record
    {
        [Required]
        public int Index { get; set; }

        [Required]
        [MaxLength(50)]
        public string User_ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Email_Address { get; set; }
    }
}
