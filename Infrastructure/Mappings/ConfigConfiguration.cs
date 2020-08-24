using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Configs;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class ConfigConfiguration : EntityTypeConfiguration<Config>
    {
        public ConfigConfiguration()
        {
            HasKey( c => c.Id );

            Property( c => c.Param ).HasMaxLength( 255 );
            Property( c => c.ParamValue ).HasMaxLength( 255 );
            Property( c => c.Description ).HasMaxLength( 255 );

            HasIndex( d => d.Param ).IsUnique();
        }
    }
}
