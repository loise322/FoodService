using System;
using TravelLine.Food.Core.Dishes;

namespace TravelLine.Food.Core.Orders
{
    public class OrderItemModel
    {
        public DishModel Salat { get; set; }

        public DishModel Soup { get; set; }

        public DishModel Garnish { get; set; }

        public DishModel SecondDish { get; set; }

        public decimal Cost
        {
            get
            {
                decimal cost = 0;

                if ( Salat != null )
                {
                    cost += Salat.Cost;
                }

                if ( Soup != null )
                {
                    cost += Soup.Cost;
                }

                if ( SecondDish != null )
                {
                    cost += SecondDish.Cost;
                }

                if ( Garnish != null )
                {
                    cost += Garnish.Cost;
                }

                return cost;
            }
        }

        public decimal DishesCost
        {
            get
            {
                decimal dishesCost = 0;

                if ( Salat != null )
                {
                    dishesCost += Convert.ToDecimal( Salat.Supplier.SalatWareCost );
                }

                if ( Soup != null )
                {
                    dishesCost += Convert.ToDecimal( Soup.Supplier.SoupWareCost );
                }

                if ( SecondDish != null )
                {
                    dishesCost += Convert.ToDecimal( SecondDish.Supplier.SecondWareCost );
                }
                else if ( Garnish != null )
                {
                    dishesCost += Convert.ToDecimal( Garnish.Supplier.SecondWareCost );
                }

                return dishesCost;
            }
        }
    }
}
