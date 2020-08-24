using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.DishRatings;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class DishRatingRepository : EFGenericRepository<DishRating>, IDishRatingRepository
    {
        public DishRatingRepository( DbContext context ) : base( context ) { }

        public List<DishRating> GetByDish( int dishId )
        {
            return QueryReadOnly().Where( dr => dr.DishId == dishId ).ToList();
        }

        public DishRating Find( int dishId, int userId )
        {
            return Query().FirstOrDefault( dr => dr.DishId == dishId && dr.UserId == userId );
        }
    }
}
