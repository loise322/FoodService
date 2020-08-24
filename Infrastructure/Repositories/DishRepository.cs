using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class DishRepository : EFGenericRepository<Dish>, IDishRepository
    {
        public DishRepository( DbContext context ) : base( context ) { }

        public List<Dish> Get( IEnumerable<int> ids )
        {
            return Query().Where( dish => ids.Contains( dish.Id ) ).OrderBy( d => d.Name ).ToList();
        }

        public List<Dish> GetByType( DishType type )
        {
            return QueryReadOnly().Where( dish => dish.Type == type ).OrderBy( d => d.Name ).ToList();
        }

        public List<Dish> GetSupplierDishes( int supplierId )
        {
            return QueryReadOnly().Where( dish => dish.SupplierId == supplierId ).OrderBy( d => d.Name ).ToList();
        }
    }
}
