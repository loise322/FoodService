using System.Collections.Generic;
using System.Web;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Core.Dishes
{
    public interface IDishService
    {
        List<DishModel> GetAllDishes();

        DishModel GetDish( int id );

        List<DishModel> GetDishesByIds( IEnumerable<int> ids );

        List<DishModel> GetDishesByType( DishType type );

        List<DishModel> GetSupplierDishes( int supplierId );

        void Remove( int dishId );

        void Save( DishModel model );

        bool IsDishUsed( int id );

        bool SaveImage( DishModel dish, HttpPostedFile file );
    }
}
