using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class DishConfiguration : EntityTypeConfiguration<Dish>
    {
        public DishConfiguration()
        {
            ToTable( "Dish" );
            HasKey( d => d.Id );

            Property( d => d.Id )
                .HasColumnName( "id_dish" );

            Property( d => d.Name )
                .HasColumnName( "name" )
                .HasMaxLength( 150 );

            Property( d => d.ImagePath )
                .HasColumnName( "image_path" )
                .HasMaxLength( 255 );

            Property( d => d.IsSingle )
                .HasColumnName( "single" );

            Property( d => d.SupplierId )
                .HasColumnName( "id_supplier" );

            HasIndex( d => d.Name ).IsUnique();

            HasRequired( d => d.Supplier ).WithMany().HasForeignKey( d => d.SupplierId ).WillCascadeOnDelete( false );
        }
    }
}
