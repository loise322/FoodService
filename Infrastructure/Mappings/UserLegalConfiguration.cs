using System.Data.Entity.ModelConfiguration;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Infrastructure.Mappings
{
    internal class UserLegalConfiguration : EntityTypeConfiguration<UserLegal>
    {
        public UserLegalConfiguration()
        {
            HasKey( ut => ut.Id );

            Property( ut => ut.UserId )
                .HasColumnName( "user_id" );

            Property( ut => ut.LegalId )
                .HasColumnName( "legal_id" );

            Property( ut => ut.StartDate )
                .HasColumnName( "start_date" );

            Property( ut => ut.TransferReason )
                .HasColumnName( "transfer_reason" );

            HasRequired( ut => ut.Legal ).WithMany().HasForeignKey( ut => ut.LegalId ).WillCascadeOnDelete( false );
            HasRequired( ut => ut.User ).WithMany( u => u.UserLegals ).HasForeignKey( ut => ut.UserId ).WillCascadeOnDelete( true );
        }
    }
}
