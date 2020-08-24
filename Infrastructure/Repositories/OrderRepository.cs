using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.Orders;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class OrderRepository : EFGenericRepository<Order>, IOrderRepository
    {
        public OrderRepository( DbContext context ) : base( context ) { }

        public void Remove( DateTime date, int userId )
        {
            var orders = Query().Where( o => o.Date == date && o.UserId == userId ).ToList();
            foreach ( Order order in orders )
            {
                Remove( order );
            }
        }

        public List<Order> GetOrders( DateTime date, int userId )
        {
            return QueryReadOnly().Where( o => o.Date == date && o.UserId == userId ).ToList();
        }

        public List<Order> GetOrders( DateTime date )
        {
            return QueryReadOnly().Where( o => o.Date == date ).ToList();
        }

        public List<Order> GetOrders( DateTime date, int[] deliveryOffices )
        {
            return QueryReadOnly().Where( o => o.Date == date && deliveryOffices.Contains( o.User.DeliveryOfficeId ) ).ToList();
        }

        public List<Order> GetOrders( DateTime startDate, DateTime endDate )
        {
            return QueryReadOnly()
                .Where( o => o.Date >= startDate && o.Date <= endDate )
                .OrderBy( o => o.Date )
                .ToList();
        }

        public List<Order> GetOrders( DateTime startDate, DateTime endDate, int userId )
        {
            return QueryReadOnly()
                .Where( o => o.Date >= startDate && o.Date <= endDate && o.UserId == userId )
                .OrderBy( o => o.Date )
                .ToList();
        }

        public decimal GetOrdersSum( DateTime startDate, DateTime endDate, int userId )
        {
            return Query()
                .Where( o => o.Date >= startDate && o.Date <= endDate && o.UserId == userId )
                .Sum( o => ( decimal? )o.Cost + ( decimal? )o.DishesCost ) ?? 0;
        }

        public int GetOrderedDishes( DateTime date, DishType type, int dishId )
        {
            int count = 0;
            switch ( type )
            {
                case DishType.Salat:
                    count = Query().Where( o => o.Date == date && o.SalatId == dishId ).Count();
                    break;
                case DishType.Soup:
                    count = Query().Where( o => o.Date == date && o.SoupId == dishId ).Count();
                    break;
                case DishType.Garnish:
                    count = Query().Where( o => o.Date == date && o.GarnishId == dishId ).Count();
                    break;
                case DishType.SecondDish:
                    count = Query().Where( o => o.Date == date && o.SecondDishId == dishId ).Count();
                    break;
                default:
                    break;
            }

            return count;
        }

        public int GetOrderedDishesInOffice( DateTime date, DishType type, int dishId )
        {
            int count = 0;
            switch ( type )
            {
                case DishType.Salat:
                    count = Query().Where( o => o.Date == date && o.SalatId == dishId ).Count();
                    break;
                case DishType.Soup:
                    count = Query().Where( o => o.Date == date && o.SoupId == dishId ).Count();
                    break;
                case DishType.Garnish:
                    count = Query().Where( o => o.Date == date && o.GarnishId == dishId ).Count();
                    break;
                case DishType.SecondDish:
                    count = Query().Where( o => o.Date == date && o.SecondDishId == dishId ).Count();
                    break;
                default:
                    break;
            }

            return count;
        }

        public bool IsOrderExist( int userId, DateTime date )
        {
            return Query().Any( o => o.UserId == userId && o.Date == date );
        }

        public List<int> GetUsersWithOrders( DateTime? date )
        {
            return Query().Where( o => date == null || o.Date == date ).Select( u => u.UserId ).Distinct().ToList();
        }

        protected override IQueryable<Order> QueryReadOnly()
        {
            return base.QueryReadOnly();
            //.Include( o => o.Garnish )
            //.Include( o => o.Salat )
            //.Include( o => o.SecondDish )
            //.Include( o => o.Soup )
            //.Include( o => o.User );

        }
    }
}
