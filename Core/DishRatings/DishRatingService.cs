using System.Linq;
using TravelLine.Food.Domain.DishRatings;

namespace TravelLine.Food.Core.DishRatings
{
    public class DishRatingService : IDishRatingService
    {
        private readonly IDishRatingRepository _dishRatingRepository;

        public DishRatingService( IDishRatingRepository dishRatingRepository )
        {
            _dishRatingRepository = dishRatingRepository;
        }

        public DishRatingModel GetDishRating( int dishId )
        {
            var ratings = _dishRatingRepository.GetByDish( dishId );

            return new DishRatingModel()
            {
                DishId = dishId,
                Dislikes = ratings.Count( r => !r.IsLiked ),
                Likes = ratings.Count( r => r.IsLiked )
            };
        }

        public bool? GetUserIsLiked( int dishId, int userId )
        {
            var rating = _dishRatingRepository.Find( dishId, userId );

            return rating?.IsLiked;
        }

        public void SetDishRating( int dishId, int userId, bool isLiked )
        {
            var rating = new DishRating()
            {
                DishId = dishId,
                UserId = userId,
                IsLiked = isLiked
            };

            _dishRatingRepository.Save( rating );
        }
    }
}
