using System;

namespace TravelLine.Food.Core.DishRatings
{
    public class DishRatingModel
    {
        public int DishId { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public override string ToString()
        {
            return String.Format( "{0},{1}", Likes, Dislikes );
        }
    }
}
