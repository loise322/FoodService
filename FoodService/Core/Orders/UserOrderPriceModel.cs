using System;

namespace TravelLine.Food.Core.Orders
{
    public class UserOrderPriceModel
    {
        public DateTime Date { get; set; }

        public decimal Cost { get; set; }

        public decimal DishesCost { get; set; }

        public decimal Total => Cost + DishesCost;
    }
}
