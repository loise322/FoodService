using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Domain.DishRatings
{
    public class DishRating
    {
        public int UserId { get; set; }

        public int DishId { get; set; }

        public bool IsLiked { get; set; }

        public virtual Dish Dish { get; set; }

        public virtual User User { get; set; }
    }
}
