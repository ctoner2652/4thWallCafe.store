using System.ComponentModel.DataAnnotations;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.API.Model
{
    public class AddCategory
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; } = null!;
    }
}
