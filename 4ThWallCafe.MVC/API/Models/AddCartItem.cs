namespace _4ThWallCafe.API.Model
{
    public class AddCartItem
    {

        public int ItemId { get; set; }

        public Guid UserSessionId { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
