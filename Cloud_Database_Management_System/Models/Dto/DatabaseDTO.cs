using System.ComponentModel.DataAnnotations;

namespace Cloud_Database_Management_System.Models.Dto
{
    public class DatabaseDTO
    {
        public int DatabaseId { get; set; }

        public string DatabaseVersion { get; set; }

        public string DatabaseType { get; set; }

        [Required]          // DataAnnotation can use when using [ApiController] attribute in controller if not it will not have any effect
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
