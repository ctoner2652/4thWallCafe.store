namespace _4ThWallCafe.MVC.Models
{
    public class ManagerHomeModel
    {
        public int todayOrders { get; set; }
        public decimal? todaySales {  get; set; }
        public decimal? monthlySales { get; set; }
        public int allTimeOrders { get; set; }
        public string employeeOfMonth {  get; set; }
    }
}
