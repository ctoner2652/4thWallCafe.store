using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _4ThWallCafe.MVC.Models
{
    public class EditItemModel
    {

        public int ItemID { get; set; }
        public int ItemPriceID { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Item Name cannot exceed 50 characters.")]
        public string ItemName { get; set; } = null!;

        [Required]
        [MaxLength(255, ErrorMessage = "Item Description cannot exceed 255 characters.")]
        public string ItemDescription { get; set; } = null!;

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int TimeOfDayId { get; set; }

        [Required]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between 0.01 and 9999.99.")]
        public decimal Price { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }

        public SelectList? Categories { get; set; } = null!;
        public SelectList? TimesOfDay { get; set; } = null!;
    }
}
