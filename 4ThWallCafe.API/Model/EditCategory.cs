using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.API.Model
{
    public class EditCategory
    {
        public int CategoryID {  get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; } = null!;
    }
}
