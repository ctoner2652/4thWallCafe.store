using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.MVC.Models
{
    public class CreateNewServer
    {
        [Required]
        [MaxLength(25, ErrorMessage = "First Name cannot exceed 25 characters.")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "Last Name cannot exceed 25 characters.")]
        public string LastName { get; set; }
        [Required]
        public DateOnly DoB { get; set; }
    }
}
