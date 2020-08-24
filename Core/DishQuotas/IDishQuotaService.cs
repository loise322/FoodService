using System;
using TravelLine.Food.Core.Dishes;

namespace TravelLine.Food.Core.DishQuotas
{
    public interface IDishQuotaService
    {
        DishQuotaModel GetDishQuota( int dishId, DateTime date );

        int GetAvailableDishQuota( int userId, DateTime date, DishModel dish );

        void SetDishQuota( int dishId, DateTime date, int quota );

        void RemoveDishQuota( int dishId, DateTime date );
    }
}
