using System;
using TravelLine.Food.Core.Dishes;

namespace TravelLine.Food.Core.Menus
{
    public interface IMenuService
    {
        MenuModel GetMenu( DateTime date );

        DayStatus GetStatus( DateTime date );

        MenuModel GetLastMenu();

        void OpenMenu( DateTime date );

        void CloseMenu( DateTime date );

        void Save( MenuModel model );
    }
}
