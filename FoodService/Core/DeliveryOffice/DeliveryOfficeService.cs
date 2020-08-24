using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Core.DeliveryOffices
{
    public class DeliveryOfficeService : IDeliveryOfficeService
    {
        public const int DefaultDeliveryOffice = 1;

        private readonly IDeliveryOfficeRepository _deliveryOfficeRepository;

        public DeliveryOfficeService( IDeliveryOfficeRepository DeliveryOfficeRepository )
        {
            _deliveryOfficeRepository = DeliveryOfficeRepository;
        }

        public int[] GetAllDeliveryOfficeIds()
        {
            return _deliveryOfficeRepository.GetAll().Select( x => x.Id ).ToArray();
        }

        public string GetDeliveryOfficeName( int id )
        {
            return _deliveryOfficeRepository.Get( id )?.Name;
        }

        public List<DeliveryOffice> GetDeliveryOffices()
        {
            return _deliveryOfficeRepository.GetAll();
        }
    }
}
