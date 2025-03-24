using System.ComponentModel.DataAnnotations;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.API.Model
{
    public class AddItem
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "CategoryID is required")]
        public int CategoryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Item Name is required")]
        public string ItemName { get; set; } = null!;

        [Required(AllowEmptyStrings = false, ErrorMessage = "ItemDescription is required")]
        public string ItemDescription { get; set; } = null!;
    }
}
