using _4ThWallCafe.MVC.Core.Entities;

namespace _4ThWallCafe.API.Model
{
    public class EditItemPrice
    {
        public int ItemPriceID { get; set; }

        public int ItemId { get; set; }

        public int TimeOfDayId { get; set; }

        public decimal Price { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly? EndDate { get; set; }
    }
}
