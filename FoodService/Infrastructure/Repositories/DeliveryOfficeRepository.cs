using System.Data.Entity;
using TravelLine.Food.Domain.DeliveryOffices;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class DeliveryOfficeRepository : EFGenericRepository<DeliveryOffice>, IDeliveryOfficeRepository
    {
        public DeliveryOfficeRepository( DbContext context ) : base( context ) { }
    }
}
