namespace TravelLine.Food.Core.DeliveryOffices
{
    public class DeliveryOfficeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Распределение квоты блюд
        /// </summary>
        public int QuotaAllocation { get; set; }
    }
}
