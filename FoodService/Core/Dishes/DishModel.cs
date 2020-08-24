using System;
using System.Text;
using System.Text.RegularExpressions;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.DishRatings;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Core.Dishes
{
    public class DishModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public DishType Type { get; set; }

        public int Weight { get; set; }

        public string ImagePath { get; set; }

        public bool IsSingle { get; set; }

        public DishRatingModel Rating { get; set; }

        public bool? UserIsLiked { get; set; }

        public int SupplierId { get; set; }

        public SupplierModel Supplier { get; set; }

        public string NameCost
        {
            get { return String.Format( "{0} ({1})", Name, Cost.ToString() ); }
        }
    }
}
