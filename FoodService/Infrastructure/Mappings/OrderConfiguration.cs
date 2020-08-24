using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Orders;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable( "Orders" );
            HasKey( o => o.Id );

            Property( o => o.Id )
                .HasColumnName( "id_order" );

            Property( o => o.Date )
                .HasColumnName( "order_date" );

            Property( o => o.UserId )
                .HasColumnName( "id_user" );

            Property( o => o.LegalId )
                .HasColumnName( "id_legal" );

            Property( o => o.SalatId )
               .HasColumnName( "salat" );

            Property( o => o.SalatCost )
               .HasColumnName( "salat_cost" );

            Property( o => o.SoupId )
               .HasColumnName( "soup" );

            Property( o => o.SoupCost )
               .HasColumnName( "soup_cost" );

            Property( o => o.GarnishId )
               .HasColumnName( "garnish" );

            Property( o => o.GarnishCost )
               .HasColumnName( "garhish_cost" );

            Property( o => o.SecondDishId )
               .HasColumnName( "second_dish" );

            Property( o => o.SecondDishCost )
               .HasColumnName( "second_dish_cost" );

            Property( d => d.Cost )
                .HasColumnName( "cost" )
                .HasPrecision( 10, 2 );

            Property( d => d.DishesCost )
                .HasColumnName( "dishes_cost" )
                .HasPrecision( 10, 2 );

            HasOptional( d => d.Garnish ).WithMany().HasForeignKey( d => d.GarnishId ).WillCascadeOnDelete( false );
            HasOptional( d => d.Salat ).WithMany().HasForeignKey( d => d.SalatId ).WillCascadeOnDelete( false );
            HasOptional( d => d.SecondDish ).WithMany().HasForeignKey( d => d.SecondDishId ).WillCascadeOnDelete( false );
            HasOptional( d => d.Soup ).WithMany().HasForeignKey( d => d.SoupId ).WillCascadeOnDelete( false );

            HasRequired( d => d.User ).WithMany().HasForeignKey( d => d.UserId ).WillCascadeOnDelete( false );
        }
    }
}
