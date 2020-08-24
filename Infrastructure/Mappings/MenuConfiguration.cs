using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Menus;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class MenuConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuConfiguration()
        {
            HasKey( d => d.Id );

            Property( d => d.Id )
                .HasColumnName( "id" );

            Property( d => d.Date )
                 .HasColumnName( "date" );

            Property( d => d.Value )
                 .HasColumnName( "menu" );

            Property( d => d.IsOrdered )
                 .HasColumnName( "is_ordered" );
 
            HasIndex( d => d.Date ).IsUnique();
        }
    }
}
