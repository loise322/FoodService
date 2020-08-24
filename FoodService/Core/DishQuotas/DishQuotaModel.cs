using System;

namespace TravelLine.Food.Core.DishQuotas
{
    public class DishQuotaModel
    {
        public int DishId { get; set; }

        public DateTime Date { get; set; }

        public int Quota { get; set; }
    }
}
