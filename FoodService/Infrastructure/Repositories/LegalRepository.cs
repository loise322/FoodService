using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class LegalRepository : EFGenericRepository<Legal>, ILegalRepository
    {
        public LegalRepository( DbContext context ) : base( context ) { }
    }
}
