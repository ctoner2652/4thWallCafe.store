using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.MVC.Models
{
    public class DisplayReportModel
    {
        public List<CafeOrder> Orders { get; set; }
        public bool HasItem { get; set; }
        public bool HasCategory {  get; set; }
        public decimal totalRevenue { get; set; }
        public string title { get; set; }
    }
}
