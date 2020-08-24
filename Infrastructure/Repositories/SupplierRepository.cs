using System.Data.Entity;
using TravelLine.Food.Domain.Suppliers;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class SupplierRepository : EFGenericRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository( DbContext context ) : base( context ) { }
    }
}
