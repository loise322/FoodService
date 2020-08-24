using System;
using System.Collections.Generic;

namespace TravelLine.Food.Domain.Dishes
{
    public interface IDishRepository
    {
        List<Dish> GetAll();

        Dish Get( int id );

        List<Dish> Get( IEnumerable<int> ids );

        List<Dish> GetByType( DishType type );

        List<Dish> GetSupplierDishes( int supplierId );

        void Remove( Dish item );

        void Save( Dish item );
    }
}
