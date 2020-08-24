using System;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public int? SalatId { get; set; }

        public decimal? SalatCost { get; set; }

        public int? SoupId { get; set; }

        public decimal? SoupCost { get; set; }

        public int? GarnishId { get; set; }

        public decimal? GarnishCost { get; set; }

        public int? SecondDishId { get; set; }

        public decimal? SecondDishCost { get; set; }

        public int LegalId { get; set; }

        public decimal Cost { get; set; }

        public decimal DishesCost { get; set; }

        public virtual Dish Garnish { get; set; }

        public virtual Dish Salat { get; set; }

        public virtual Dish SecondDish { get; set; }

        public virtual Dish Soup { get; set; }

        public virtual User User { get; set; }
    }
}
