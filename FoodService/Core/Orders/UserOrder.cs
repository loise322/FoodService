using System;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Core.Orders
{
    public class UserOrder
    {
        public Legal Legal { get; set; }

        public UserModel User { get; set; }

        public DateTime Date { get; set; }

        public Decimal Cost { get; set; }

        public Decimal DishesCost { get; set; }
    }
}
