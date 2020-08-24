using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.DishRatings;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class DishRatingConfiguration : EntityTypeConfiguration<DishRating>
    {
        public DishRatingConfiguration()
        {
            HasKey( dr => new { dr.DishId, dr.UserId } );

            HasRequired( dr => dr.Dish ).WithMany().HasForeignKey( dr => dr.DishId );
            HasRequired( dr => dr.User ).WithMany().HasForeignKey( dr => dr.UserId );

            Property( dr => dr.DishId ).HasColumnName( "id_dish" );
            Property( dr => dr.UserId ).HasColumnName( "id_user" );
            Property( dr => dr.IsLiked ).HasColumnName( "is_liked" );

        }
    }
}
