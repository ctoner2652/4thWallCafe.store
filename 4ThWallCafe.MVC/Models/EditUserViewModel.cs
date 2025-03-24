using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.MVC.Models
{
    public class EditUserViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "User Name cannot exceed 50 characters.")]
        public string UserName { get; set; }

        public bool IsManager { get; set; }
        public bool IsAccountant { get; set; }
    }
}
