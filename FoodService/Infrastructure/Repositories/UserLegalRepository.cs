using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class UserLegalRepository : EFGenericRepository<UserLegal>, IUserLegalRepository
    {
        public UserLegalRepository( DbContext context ) : base( context ) { }

        public UserLegal GetUserLegal( int id )
        {
            return Query().FirstOrDefault( ul => ul.Id == id );
        }
    }
}
