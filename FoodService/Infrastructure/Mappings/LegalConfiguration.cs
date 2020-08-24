using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class LegalConfiguration : EntityTypeConfiguration<Legal>
    {
        public LegalConfiguration()
        {
            HasKey( t => t.Id );

            Property( t => t.Name )
                .HasColumnName( "name" )
                .HasMaxLength( 255 );

            Property( t => t.FullName )
                .HasColumnName( "full_name" )
                .HasMaxLength( 255 );

            Property( u => u.Code )
                .HasColumnName( "code" )
                .HasMaxLength( 10 );

            Property( u => u.ExternalId )
                .HasColumnName( "id_external" )
                .HasMaxLength( 50 );

            Property( t => t.IsDeleted )
                .HasColumnName( "is_deleted" );
        }
    }
}
