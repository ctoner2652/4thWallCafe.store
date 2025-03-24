namespace _4ThWallCafe.API.Model
{
    public class EditCafeOrder
    {
        public int OrderId { get; set; }

        public int? ServerId { get; set; }

        public int PaymentTypeId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal? SubTotal { get; set; }

        public decimal? Tax { get; set; }

        public decimal? Tip { get; set; }

        public decimal? AmountDue { get; set; }
    }
}
