using System.ComponentModel.DataAnnotations;

namespace _4ThWallCafe.API.Model
{
    public class EditItem
    {
        public int ItemId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "CategoryID is required")]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Item Name is required")]
        public string ItemName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "ItemDescription is required")]
        public string ItemDescription { get; set; } = null!;
    }
}
