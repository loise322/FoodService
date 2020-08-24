using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Core.DeliveryOffices
{
    internal static class DeliveryOfficeConverter
    {
        internal static DeliveryOfficeModel Convert( DeliveryOffice deliveryOffice )
        {
            if ( deliveryOffice == null )
            {
                return null;
            }

            var result = new DeliveryOfficeModel()
            {
                Id = deliveryOffice.Id,
                Name = deliveryOffice.Name,
                QuotaAllocation = deliveryOffice.QuotaAllocation
            };

            return result;
        }

        internal static DeliveryOffice Convert( DeliveryOfficeModel model )
        {
            if ( model == null )
            {
                return null;
            }

            var user = new DeliveryOffice()
            {
                Id = model.Id,
                Name = model.Name,
                QuotaAllocation = model.QuotaAllocation
            };

            return user;
        }
    }
}
