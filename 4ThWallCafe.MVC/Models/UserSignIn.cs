using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.MVC.Models
{
    public class UserSignIn
    {
        [Required]
        public string Username {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
