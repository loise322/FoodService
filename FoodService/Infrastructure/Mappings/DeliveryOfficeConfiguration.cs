using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class DeliveryOfficeConfiguration : EntityTypeConfiguration<DeliveryOffice>
    {
        public DeliveryOfficeConfiguration()
        {
            HasKey( g => g.Id );

            Property( g => g.Id )
                .HasColumnName( "id_delivery_office" );

            Property( g => g.Name )
                .HasColumnName( "name" )
                .IsRequired()
                .HasMaxLength( 50 );

            Property( g => g.QuotaAllocation )
                .HasColumnName( "quota_allocation" );
        }
    }
}
