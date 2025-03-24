using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.MVC.Models
{
    public class CreateNewAPIUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password " +
            "do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
