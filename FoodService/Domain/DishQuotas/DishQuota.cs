using System;

namespace TravelLine.Food.Domain.DishQuotas
{
    public class DishQuota
    {
        public int DishId { get; set; }

        public DateTime Date { get; set; }

        public int Quota { get; set; }
    }
}
