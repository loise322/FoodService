using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.WebApi.Models;

namespace TravelLine.Food.WebApi.Controllers
{
    [RoutePrefix( "order" )]
    public class OrderController : ApiController
    {
        private readonly ICalendarService _calendarService;
        private readonly IDishService _dishService;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderController( 
            ICalendarService calendarService,
            IDishService dishService,
            IMenuService menuService, 
            IOrderService orderService, 
            IUserService userService )
        {
            _calendarService = calendarService;
            _dishService = dishService;
            _menuService = menuService;
            _orderService = orderService;
            _userService = userService;
        }

        [HttpPost]
        [Route( "save-order" )]
        public IHttpActionResult SaveOrder( [FromBody] OrderDto orderDto )
        {
            if ( orderDto == null )
            {
                return BadRequest( "No order to save" );
            }

            List<DateTime> availableDates = _calendarService.GetAvailableDates();
            if ( !availableDates.Contains( orderDto.Date ) )
            {
                return BadRequest( "Date for order not available" );
            }

            OrderModel order = GetOrder( orderDto );
            if ( order == null )
            {
                return BadRequest( "Order is incorrect" );
            }

            _orderService.Save( order );

            return Ok();
        }

        private OrderModel GetOrder( OrderDto orderDto )
        {
            UserModel user = _userService.GetUser( orderDto.UserId );
            MenuModel menu = _menuService.GetMenu( orderDto.Date );

            OrderModel order = new OrderModel()
            {
                User = user,
                Date = orderDto.Date,
                Items = new List<OrderItemModel>(),
            };

            foreach ( OrderItemDto itemDto in orderDto.Items )
            {
                if ( itemDto.Dishes.Count > 4 )
                {
                    return null;
                }

                DishModel salad = null;
                DishModel soup = null;
                DishModel secondDish = null;
                DishModel garnish = null;
                foreach ( DishDto dishDto in itemDto.Dishes )
                {
                    if ( menu.Dishes.All( d => d.Id != dishDto.Id ) )
                    {
                        return null;
                    }

                    DishModel dish = _dishService.GetDish( dishDto.Id );
                    switch ( dish.Type )
                    {
                        case DishType.Salat:
                            if ( salad != null )
                            {
                                return null;
                            }
                            salad = dish;
                            break;
                        case DishType.Soup:
                            if ( soup != null )
                            {
                                return null;
                            }
                            soup = dish;
                            break;
                        case DishType.SecondDish:
                            if ( secondDish != null )
                            {
                                return null;
                            }
                            secondDish = dish;
                            break;
                        case DishType.Garnish:
                            if ( garnish != null )
                            {
                                return null;
                            }
                            garnish = dish;
                            break;
                    }
                }

                var orderItem = new OrderItemModel()
                {
                    Salat = salad,
                    Soup = soup,
                    SecondDish = secondDish,
                    Garnish = garnish
                };
                order.Items.Add( orderItem );
            }

            return order;
        }

        [HttpGet]
        [Route( "get-order" )]
        public IHttpActionResult GetOrder( int userId, DateTime date )
        {
            OrderModel order = _orderService.GetOrder( userId, date );

            if ( order == null )
            {
                return Ok();
            }

            return Ok( GetOrderDto( order ) );
        }

        private OrderDto GetOrderDto( OrderModel order )
        {
            var orderDto = new OrderDto
            {
                Date = order.Date,
                UserId = order.User.Id,
                Items = new List<OrderItemDto>()
            };

            foreach ( OrderItemModel orderItem in order.Items )
            {
                OrderItemDto orderItemDto = new OrderItemDto()
                {
                    Dishes = new List<DishDto>()
                };

                if ( orderItem.Salat != null )
                {
                    orderItemDto.Dishes.Add( GetDishDto( orderItem.Salat ) );
                }

                if ( orderItem.Soup != null )
                {
                    orderItemDto.Dishes.Add( GetDishDto( orderItem.Soup ) );
                }

                if ( orderItem.SecondDish != null )
                {
                    orderItemDto.Dishes.Add( GetDishDto( orderItem.SecondDish ) );
                }

                if ( orderItem.Garnish != null )
                {
                    orderItemDto.Dishes.Add( GetDishDto( orderItem.Garnish ) );
                }

                orderDto.Items.Add( orderItemDto );
            }

            return orderDto;
        }

        private DishDto GetDishDto( DishModel dish )
        {
            return new DishDto()
            {
                Id = dish.Id,
                Name = dish.Name,
                Description = dish.Description,
                Price = dish.Cost,
                Type = (int)dish.Type,
                Weight = dish.Weight
            };
        }
    }
}
