using System;
using System.Collections.Generic;
using System.Web.Http;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.WebApi.Models;

namespace TravelLine.Food.WebApi.Controllers
{
    [RoutePrefix( "menu" )]
    public class MenuController : ApiController
    {
        private readonly ICalendarService _calendarService;
        private readonly IMenuService _menuService;

        public MenuController( ICalendarService calendarService, IMenuService menuService )
        {
            _calendarService = calendarService;
            _menuService = menuService;
        }

        [HttpGet]
        [Route( "available-dates" )]
        public IHttpActionResult AvailableDates()
        {
            return Ok( _calendarService.GetAvailableDates() );
        }

        [HttpGet]
        [Route( "available-dishes" )]
        public IHttpActionResult AvailableDishes( DateTime date )
        {
            MenuModel menu = _menuService.GetMenu( date );
            if ( menu == null )
            {
                return Ok();
            }

            MenuDto menuDto = new MenuDto()
            {
                Date = menu.Date,
                Dishes = new List<DishDto>()
            };

            foreach ( DishModel dish in menu.Dishes )
            {
                menuDto.Dishes.Add( GetDishDto( dish ) );
            }
            return Ok( menuDto );
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
