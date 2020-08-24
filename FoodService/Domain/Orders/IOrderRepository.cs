using System;
using System.Collections.Generic;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Domain.Orders
{
    public interface IOrderRepository
    {
        List<Order> GetAll();

        Order Get( int id );

        List<Order> GetOrders( DateTime date, int userId );

        List<Order> GetOrders( DateTime date );

        List<Order> GetOrders( DateTime date, int[] deliveryOffices );

        List<Order> GetOrders( DateTime startDate, DateTime endDate );

        List<Order> GetOrders( DateTime startDate, DateTime endDate, int userId );

        decimal GetOrdersSum( DateTime startDate, DateTime endDate, int userId );

        int GetOrderedDishes( DateTime date, DishType type, int dishId );

        int GetOrderedDishesInOffice( DateTime date, DishType type, int dishId );

        List<int> GetUsersWithOrders( DateTime? date = null );

        bool IsOrderExist( int userId, DateTime date );

        void Remove( Order item );

        void Remove( DateTime date, int userId );

        void Save( Order item );

        void Save( List<Order> items );
    }
}
