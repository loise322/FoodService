using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Orders;

namespace TravelLine.Food.Core.Orders
{
    internal static class OrderConverter
    {
        internal static OrderModel Convert( DateTime date, UserModel user, List<Order> orders )
        {
            var result = new OrderModel()
            {
                Date = date,
                Items = new List<OrderItemModel>(),
                LegalId = orders[ 0 ].LegalId,
                User = user
            };

            foreach ( Order order in orders )
            {
                result.Items.Add( new OrderItemModel()
                {
                    Garnish = DishConverter.Convert( order.Garnish, order.GarnishCost ),
                    Salat = DishConverter.Convert( order.Salat, order.SalatCost ),
                    SecondDish = DishConverter.Convert( order.SecondDish, order.SecondDishCost ),
                    Soup = DishConverter.Convert( order.Soup, order.SoupCost )
                } );
            }

            return result;
        }

        internal static List<OrderModel> Convert( DateTime date, List<Order> orders, List<UserModel> users )
        {
            var result = new List<OrderModel>();

            var orderGroups = orders.GroupBy( o => new { o.Date, o.UserId } );
            foreach ( var orderGroup in orderGroups )
            {
                Order firstUserOrder = orderGroup.First();
                result.Add( Convert( orderGroup.Key.Date, users.Find( u => u.Id == firstUserOrder.UserId ), orderGroup.ToList() ) );
            }

            return result.OrderBy( r => r.User.Name ).ToList();
        }

        internal static List<Order> Convert( OrderModel model )
        {
            var result = new List<Order>();

            foreach ( OrderItemModel item in model.Items )
            {
                result.Add( new Order()
                {
                    Date = model.Date,
                    UserId = model.User.Id,
                    LegalId = model.User.GetUserLegal( model.Date ).LegalId,
                    SalatId = item.Salat?.Id,
                    SalatCost = item.Salat?.Cost,
                    SecondDishId = item.SecondDish?.Id,
                    SecondDishCost = item.SecondDish?.Cost,
                    SoupId = item.Soup?.Id,
                    SoupCost = item.Soup?.Cost,
                    GarnishId = item.Garnish?.Id,
                    GarnishCost = item.Garnish?.Cost,
                    Cost = item.Cost,
                    DishesCost = item.DishesCost
                } );
            }

            return result;
        }
    }
}
