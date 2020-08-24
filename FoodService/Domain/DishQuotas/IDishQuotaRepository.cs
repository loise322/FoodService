using System;
using System.Collections.Generic;

namespace TravelLine.Food.Domain.DishQuotas
{
    public interface IDishQuotaRepository
    {
        DishQuota GetDishQuota( int dishId, DateTime date );

        void Save( DishQuota dishQuota );

        void Remove( DishQuota dishQuota );
    }
}
