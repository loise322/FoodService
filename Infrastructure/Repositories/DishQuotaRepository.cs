using System;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.DishQuotas;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class DishQuotaRepository : EFGenericRepository<DishQuota>, IDishQuotaRepository
    {
        public DishQuotaRepository( DbContext context ) : base( context ) { }

        public DishQuota GetDishQuota( int dishId, DateTime date )
        {
            return Query().FirstOrDefault( dr => dr.DishId == dishId && dr.Date == date );
        }
    }
}
