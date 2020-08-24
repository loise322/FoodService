using System.Collections.Generic;

namespace TravelLine.Food.Domain.DeliveryOffices
{
    public interface IDeliveryOfficeRepository
    {
        DeliveryOffice Get( int id );

        List<DeliveryOffice> GetAll();
    }
}
