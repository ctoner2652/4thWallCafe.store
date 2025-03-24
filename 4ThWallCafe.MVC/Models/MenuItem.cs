namespace _4ThWallCafe.MVC.Models
{
    public class MenuItem
    {
        public int ItemID {  get; set; }
        public string ItemName { get; set; }
        public int ItemPriceID { get; set; }
        public string ItemDescription { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string TimeOfDayName { get; set; }
    }
}
