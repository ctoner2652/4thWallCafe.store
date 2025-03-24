using System.ComponentModel.DataAnnotations;
using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.Models
{
    public class ReportRequestModel
    {
        [Required]
        public string TimeRange { get; set; } 

        public int? CategoryId { get; set; } 

        public int? ItemId { get; set; } 

        public List<Category> categories { get; set; }
        public List<Item> items { get; set; }
    }
}
