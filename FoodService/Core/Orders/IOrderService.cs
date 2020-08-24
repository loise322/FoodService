using System;
using System.Collections.Generic;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Core.Orders
{
    public interface IOrderService
    {
        OrderModel GetOrder( int userId, DateTime date );

        List<OrderModel> GetOrders( DateTime date );

        List<OrderModel> GetOrdersByDeliveryOffice( DateTime date, int deliveryOffice, bool? isStudents = null );

        List<OrderModel> GetOrdersByLegal( DateTime date, int legalId );

        List<UserOrder> GetOrders( DateTime startDate, DateTime endDate );

        List<UserOrderPriceModel> GetUserOrders( DateTime startDate, DateTime endDate, int userId );

        decimal GetUserOrdersSum( DateTime startDate, DateTime endDate, int userId );

        int GetOrderedDishes( DateTime date, DishType type, int dishId );

        int GetOrderedDishesInOffice( DateTime date, DishType type, int dishId );

        bool IsOrderExist( int userId, DateTime date );

        void Save( OrderModel order );
    }
}
