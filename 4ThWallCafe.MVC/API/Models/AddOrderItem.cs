namespace _4ThWallCafe.API.Model
{
    public class AddOrderItem
    {
        public int OrderId { get; set; }

        public int ItemPriceId { get; set; }

        public byte Quantity { get; set; }

        public decimal ExtendedPrice { get; set; }
    }
}
