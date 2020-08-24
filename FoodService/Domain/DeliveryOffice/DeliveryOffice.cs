namespace TravelLine.Food.Domain.DeliveryOffices
{
    /// <summary>
    /// Группа (Офис)
    /// </summary>
    public class DeliveryOffice
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Распределение квоты блюд
        /// </summary>
        public int QuotaAllocation { get; set; }
    }
}
