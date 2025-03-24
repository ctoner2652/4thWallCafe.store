using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.MVC.Models
{
    public class EditServerModel
    {

        public int ServerID { get; set; }

        [Required]
        [MaxLength(25, ErrorMessage = "First Name cannot exceed 25 characters.")]
        public string FirstName { get; set; } = null!;
        [Required]
        [MaxLength(25, ErrorMessage = "Last Name cannot exceed 25 characters.")]
        public string LastName { get; set; } = null!;
        [Required]
        public DateOnly HireDate { get; set; }

        public DateOnly? TermDate { get; set; }
        [Required]
        public DateOnly DoB { get; set; }
    }
}
