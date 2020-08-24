using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.DishQuotas;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class DishQuotaConfiguration : EntityTypeConfiguration<DishQuota>
    {
        public DishQuotaConfiguration()
        {
            HasKey( dq => new { dq.DishId, dq.Date } );

            Property( dr => dr.DishId ).HasColumnName( "id_dish" );
            Property( dr => dr.Date ).HasColumnName( "date" );
            Property( dr => dr.Quota ).HasColumnName( "quota" );
        }
    }
}
