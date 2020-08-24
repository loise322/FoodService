namespace TravelLine.Food.Core.DishRatings
{
    public interface IDishRatingService
    {
        DishRatingModel GetDishRating( int dishId );

        bool? GetUserIsLiked( int dishId, int userId );

        void SetDishRating( int dishId, int userId, bool isLiked );
    }
}
