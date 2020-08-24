using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            HasKey( u => u.Id );

            Property( u => u.Id )
                .HasColumnName( "id_user" );

            Property( u => u.Name )
                .HasColumnName( "name" )
                .HasMaxLength( 255 );

            Property( u => u.DeliveryOfficeId )
                .HasColumnName( "id_delivery_office" );

            Property( u => u.Login )
                .HasColumnName( "login" )
                .HasMaxLength( 255 );

            Property( u => u.Code )
                .HasColumnName( "code" )
                .HasMaxLength( 10 );

            Property( u => u.ExternalId )
                .HasColumnName( "id_external" )
                .HasMaxLength( 50 );

            Property( u => u.IsEnabled )
                .HasColumnName( "enabled" );

            HasRequired( u => u.DeliveryOffice ).WithMany().HasForeignKey( u => u.DeliveryOfficeId );
        }
    }
}
