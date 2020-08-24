using System;
using System.Collections.Generic;

namespace TravelLine.Food.Domain.Menus
{
    public interface IMenuRepository
    {
        List<Menu> GetAll();

        Menu Get( int id );

        Menu Get( DateTime date );

        Menu GetLastMenu();

        void Remove( Menu item );

        void Save( Menu item );
    }
}
