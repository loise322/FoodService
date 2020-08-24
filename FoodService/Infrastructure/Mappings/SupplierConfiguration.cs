using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Suppliers;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class SupplierConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierConfiguration()
        {
            HasKey( t => t.Id );

            Property( t => t.Name )
                .HasColumnName( "name" )
                .HasMaxLength( 255 );

            Property( t => t.Address )
                .HasColumnName( "address" )
                .HasMaxLength( 255 );

            Property( t => t.ContactPerson )
                .HasColumnName( "contact_person" )
                .HasMaxLength( 255 );

            Property( t => t.Email )
                .HasColumnName( "email" )
                .HasMaxLength( 100 );

            Property( t => t.Phone )
                .HasColumnName( "phone" )
                .HasMaxLength( 50 );

            Property( t => t.LegalEntity )
                .HasColumnName( "legal_entity" )
                .HasMaxLength( 255 );

            Property( t => t.Discount )
                .HasColumnName( "discount" );

            Property( t => t.SalatWareCost )
                .HasColumnName( "salat_ware_cost" );

            Property( t => t.SoupWareCost )
                .HasColumnName( "soup_ware_cost" );

            Property( t => t.SecondWareCost )
                .HasColumnName( "second_ware_cost" );
        }
    }
}
