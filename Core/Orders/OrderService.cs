using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishQuotas;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Orders;

namespace TravelLine.Food.Core.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILegalService _legalService;
        private readonly IUserService _userService;
        private readonly ISupplierService _supplierService;
        private readonly IDishQuotaService _dishQuotaService;

        public OrderService(
            IOrderRepository orderRepository,
            ILegalService legalService,
            IUserService userService,
            ISupplierService supplierService,
            IDishQuotaService dishQuotaService )
        {
            _orderRepository = orderRepository;
            _legalService = legalService;
            _userService = userService;
            _supplierService = supplierService;
            _dishQuotaService = dishQuotaService;
        }

        public OrderModel GetOrder( int userId, DateTime date )
        {
            List<Order> orders = _orderRepository.GetOrders( date, userId );

            if ( orders == null || orders.Count == 0 )
            {
                return null;
            }

            return OrderConverter.Convert( date, UserConverter.Convert( orders[ 0 ].User ), orders );
        }

        public bool IsOrderExist( int userId, DateTime date )
        {
            return _orderRepository.IsOrderExist( userId, date );
        }

        public List<OrderModel> GetOrders( DateTime date )
        {
            List<Order> orders = _orderRepository.GetOrders( date );
            List<UserModel> users = _userService.GetAllUsers();

            return OrderConverter.Convert( date, orders, users );
        }

        public List<OrderModel> GetOrdersByDeliveryOffice( DateTime date, int deliveryOffice, bool? isStudents = null )
        {
            List<Order> orders = _orderRepository.GetOrders( date, new int[] { deliveryOffice } );
            List<UserModel> users = _userService.GetAllUsers();

            if ( isStudents != null )
            {
                if ( isStudents.Value )
                {
                    orders = orders.FindAll( o => o.LegalId == ConfigService.StudentsID );
                }
                else
                {
                    orders = orders.FindAll( o => o.LegalId != ConfigService.StudentsID );
                }
            }

            return OrderConverter.Convert( date, orders, users );
        }

        public List<OrderModel> GetOrdersByLegal( DateTime date, int legalId )
        {
            List<OrderModel> orders = GetOrders( date ).FindAll( o => o.LegalId == legalId );

            return orders;
        }

        public List<UserOrder> GetOrders( DateTime startDate, DateTime endDate )
        {
            List<Order> orders = _orderRepository.GetOrders( startDate, endDate );
            List<Legal> legals = _legalService.GetLegals();
            List<UserModel> users = _userService.GetAllUsers();

            return orders
                .GroupBy( o => new { o.Date, o.UserId, o.LegalId } )
                .Select( g => new UserOrder()
                {
                    Date = g.Key.Date,
                    Legal = legals.Find( l => l.Id == g.Key.LegalId ),
                    User = users.Find( u => u.Id == g.Key.UserId ),
                    Cost = g.Sum( o => o.Cost ),
                    DishesCost = g.Sum( o => o.DishesCost )
                } )
                .ToList();
        }

        public List<UserOrderPriceModel> GetUserOrders( DateTime startDate, DateTime endDate, int userId )
        {
            List<Order> orders = _orderRepository.GetOrders( startDate, endDate, userId );

            return orders
                .GroupBy( o => o.Date )
                .Select( g => new UserOrderPriceModel()
                {
                    Date = g.Key,
                    Cost = g.Sum( o => o.Cost ),
                    DishesCost = g.Sum( o => o.DishesCost )
                } )
                .ToList();
        }

        public decimal GetUserOrdersSum( DateTime startDate, DateTime endDate, int userId )
        {
            return _orderRepository.GetOrdersSum( startDate, endDate, userId );
        }

        public int GetOrderedDishes( DateTime date, DishType type, int dishId )
        {
            return _orderRepository.GetOrderedDishes( date, type, dishId );
        }

        public int GetOrderedDishesInOffice( DateTime date, DishType type, int dishId )
        {
            return _orderRepository.GetOrderedDishesInOffice( date, type, dishId );
        }

        public void Save( OrderModel order )
        {
            Dictionary<int, int> dishesQuota = GetDishesQuota( order );
            OrderModel existedMenu = GetOrder( order.User.Id, order.Date );

            AddQuotaForExistedDishes( ref dishesQuota, existedMenu );
            ClearUnavailableDishes( ref order, dishesQuota );

            _orderRepository.Remove( order.Date, order.User.Id );

            _orderRepository.Save( OrderConverter.Convert( order ) );
        }

        private void ClearUnavailableDishes( ref OrderModel order, Dictionary<int, int> dishesQuota )
        {
            var orderItems = new List<OrderItemModel>();
            foreach ( OrderItemModel item in order.Items )
            {
                item.Salat = GetDishIfAvailable( ref dishesQuota, item.Salat );
                item.Soup = GetDishIfAvailable( ref dishesQuota, item.Soup );
                item.SecondDish = GetDishIfAvailable( ref dishesQuota, item.SecondDish );
                item.Garnish = GetDishIfAvailable( ref dishesQuota, item.Garnish );

                if ( item.Salat != null || item.Soup != null || item.SecondDish != null || item.Garnish != null )
                {
                    orderItems.Add( item );
                }
            }

            order.Items = orderItems;
        }

        private DishModel GetDishIfAvailable( ref Dictionary<int, int> dishesQuota, DishModel dish )
        {
            if ( dish != null && dishesQuota[ dish.Id ] > 0 )
            {
                dishesQuota[ dish.Id ] -= 1;
                return dish;
            }

            return null;
        }

        private void AddQuotaForExistedDishes( ref Dictionary<int, int> dishesQuota, OrderModel existedMenu )
        {
            if ( existedMenu == null )
            {
                return;
            }

            Dictionary<int, int> existedOrderedDishes = GetOrderedDishesDictionary( existedMenu );
            foreach ( KeyValuePair<int, int> existedOrderedDish in existedOrderedDishes )
            {
                if ( dishesQuota.ContainsKey( existedOrderedDish.Key ) )
                {
                    dishesQuota[ existedOrderedDish.Key ] += existedOrderedDishes[ existedOrderedDish.Key ];
                }
            }
        }

        private Dictionary<int, int> GetDishesQuota( OrderModel order )
        {
            Dictionary<int, int> orderedDishes = new Dictionary<int, int>();

            if ( order == null )
            {
                return orderedDishes;
            }

            foreach ( OrderItemModel item in order.Items )
            {
                AddDishQuota( ref orderedDishes, order, item.Salat );
                AddDishQuota( ref orderedDishes, order, item.Soup );
                AddDishQuota( ref orderedDishes, order, item.SecondDish );
                AddDishQuota( ref orderedDishes, order, item.Garnish );
            }

            return orderedDishes;
        }

        private void AddDishQuota( ref Dictionary<int, int> orderedDishes, OrderModel order, DishModel dish )
        {
            if ( dish != null && !orderedDishes.ContainsKey( dish.Id ) )
            {
                orderedDishes[ dish.Id ] = _dishQuotaService.GetAvailableDishQuota( order.User.Id, order.Date, dish );

            }
        }

        private Dictionary<int, int> GetOrderedDishesDictionary( OrderModel model )
        {
            Dictionary<int, int> orderedDishes = new Dictionary<int, int>();
            foreach ( OrderItemModel item in model.Items )
            {
                AddOrderedDish( ref orderedDishes, item.Salat );
                AddOrderedDish( ref orderedDishes, item.Soup );
                AddOrderedDish( ref orderedDishes, item.SecondDish );
                AddOrderedDish( ref orderedDishes, item.Garnish );
            }

            return orderedDishes;
        }

        private void AddOrderedDish( ref Dictionary<int, int> orderedDishes, DishModel dish )
        {
            if ( dish != null )
            {
                if ( orderedDishes.ContainsKey( dish.Id ) )
                {
                    orderedDishes[ dish.Id ]++;
                }
                else
                {
                    orderedDishes.Add( dish.Id, 1 );
                }
            }
        }
    }
}
