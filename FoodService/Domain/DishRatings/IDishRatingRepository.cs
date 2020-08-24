using System.Collections.Generic;

namespace TravelLine.Food.Domain.DishRatings
{
    public interface IDishRatingRepository
    {
        List<DishRating> GetByDish( int dishId );

        DishRating Find( int dishId, int userId );

        void Save( DishRating rating );
    }
}
