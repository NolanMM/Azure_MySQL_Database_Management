using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Group_Data_Models 
{
    public class Group3_Data_Model : Group_Data_Model
    {
        public string stock { get; set; }

        public string sales { get; set; }

        public string rating { get; set; }

        [Required]
        public string pid { get; set; }

        public string sid { get; set; }

        public string clicks { get; set; }

        public DateTime? clicked_time { get; set; }

    }
}
