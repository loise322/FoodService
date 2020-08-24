using System;
using System.Collections.Generic;
using TravelLine.Food.Core.Users;

namespace TravelLine.Food.Core.Orders
{
    public class OrderModel
    {
        public DateTime Date { get; set; }

        public UserModel User { get; set; }

        public int LegalId { get; set; }

        public List<OrderItemModel> Items { get; set; }
    }
}
