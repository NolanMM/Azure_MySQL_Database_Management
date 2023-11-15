using System.ComponentModel.DataAnnotations;

namespace Security_Services_Dev_Env.Services.Security_Services.Security_Table.Data_Models
{
    public class Security_Password_Record : Security_Data_Model_Abtraction
    {
        [Required]
        public int Index_pass { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
        public override string ToString()
        {
            return $"Index_pass: {Index_pass}, Password: {Password}";
        }
    }
}
