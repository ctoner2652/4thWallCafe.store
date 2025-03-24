namespace _4ThWallCafe.MVC.Models
{
    public class OrderConfirmationInformation
    {
        public int OrderNumber {  get; set; }
        public DateTime ETA { get; set; }
        public List<DisplayCartItem> OrderSummary { get; set; }
        public decimal? TotalAmountPaid { get; set; }
        public string Payment { get; set; }


    }
}
