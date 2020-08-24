using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.WorkTimes;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class WorkTimeConfiguration : EntityTypeConfiguration<WorkTime>
    {
        public WorkTimeConfiguration()
        {
            ToTable( "WorkTimes" );
            HasKey( o => o.Id );

            Property( o => o.Id )
                .HasColumnName( "id_work_time" );

            Property( o => o.Month )
                .HasColumnName( "month" );

            Property( o => o.LegalId )
                .HasColumnName( "id_legal" );

            Property( o => o.UserId )
                .HasColumnName( "id_user" );

            Property( o => o.Days )
                .HasColumnName( "days" );

            HasRequired( d => d.Legal ).WithMany().HasForeignKey( d => d.LegalId ).WillCascadeOnDelete( false );
            HasRequired( d => d.User ).WithMany().HasForeignKey( d => d.UserId ).WillCascadeOnDelete( false );
        }
    }
}
