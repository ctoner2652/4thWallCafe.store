namespace _4ThWallCafe.MVC.Models
{
    public class DisplayCartItem
    {
        public int ItemId { get; set; }

        public string ItemName {  get; set; }

        public Guid UserSessionId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
