using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishQuotas;
using TravelLine.Food.Domain.Menus;

namespace TravelLine.Food.Core.Menus
{
    public class MenuService : IMenuService
    {
        private readonly IDishService _dishService;
        private readonly IDishQuotaService _dishQuotaService;
        private readonly IMenuRepository _menuRepository;

        public MenuService( 
            IDishService dishService,
            IDishQuotaService dishQuotaService,
            IMenuRepository menuRepository )
        {
            _dishService = dishService;
            _dishQuotaService = dishQuotaService;
            _menuRepository = menuRepository;
        }

        public MenuModel GetMenu( DateTime date )
        {
            Menu menu = _menuRepository.Get( date );
            if ( menu == null )
            {
                return null;
            }

            MenuModel result = Convert( menu );

            IEnumerable<int> dishIds = menu.Value.Split( ',' ).Select( d => Int32.Parse( d ) );
            result.Dishes = _dishService.GetDishesByIds( dishIds );

            return result;
        }

        public MenuModel GetLastMenu()
        {
            return Convert( _menuRepository.GetLastMenu() );
        }

        public DayStatus GetStatus( DateTime date )
        {
            Menu menu = _menuRepository.Get( date );
            if ( menu == null )
            {
                return DayStatus.Open;
            }

            return menu.IsOrdered ? DayStatus.Closed : DayStatus.PreparedForOrder;
        }

        public void OpenMenu( DateTime date )
        {
            var menu = _menuRepository.Get( date );
            if ( menu != null )
            {
                menu.IsOrdered = false;

                _menuRepository.Save( menu );
            }
        }

        public void CloseMenu( DateTime date )
        {
            var menu = _menuRepository.Get( date );
            if ( menu != null )
            {
                menu.IsOrdered = true;

                _menuRepository.Save( menu );
            }
        }

        public void Save( MenuModel model )
        {
            Menu menu = Convert( model );
            _menuRepository.Save( menu );

            if ( model.Id == 0 )
            {
                model.Id = menu.Id;
            }
        }

        private static Menu Convert( MenuModel model )
        {
            if ( model == null )
            {
                return null;
            }

            return new Menu()
            {
                Date = new DateTime( model.Date.Year, model.Date.Month, model.Date.Day ),
                Id = model.Id,
                IsOrdered = model.IsOrdered,
                Value = model.ToString()
            };
        }

        private static MenuModel Convert( Menu menu )
        {
            if ( menu == null )
            {
                return null;
            }

            return new MenuModel()
            {
                Date = new DateTime(menu.Date.Year, menu.Date.Month, menu.Date.Day),
                Id = menu.Id,
                IsOrdered = menu.IsOrdered
            };
        }
    }
}
