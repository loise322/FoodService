using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class UserRepository : EFGenericRepository<User>, IUserRepository
    {
        private readonly IUserLegalRepository _userLegalRepository;
        public UserRepository( DbContext context ) : base( context ) { }

        public User GetUserByLogin( string login )
        {
            return Query().FirstOrDefault( u => u.Login == login );
        }

        public List<User> GetUsers( int deliveryOfficeId, int legalId )
        {
            return QueryReadOnly().Include( u => u.UserLegals )
                .Where( u => ( deliveryOfficeId == 0 || u.DeliveryOfficeId == deliveryOfficeId )
                && ( legalId == 0 || u.UserLegals.Any( ul => ul.LegalId == legalId && ul.StartDate < DateTime.Today ) )
                ).OrderBy( u => u.Name ).ToList();
        }

        protected override IQueryable<User> QueryReadOnly()
        {
            return base.QueryReadOnly().Include( u => u.DeliveryOffice ).Include( u => u.UserLegals );
        }
    }
}
