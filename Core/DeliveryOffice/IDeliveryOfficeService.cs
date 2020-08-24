using System.Collections.Generic;
using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Core.DeliveryOffices
{
    public interface IDeliveryOfficeService
    {
        int[] GetAllDeliveryOfficeIds();

        List<DeliveryOffice> GetDeliveryOffices();
        string GetDeliveryOfficeName( int id );
    }
}
