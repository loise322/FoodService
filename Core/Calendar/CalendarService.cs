using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Menus;

namespace TravelLine.Food.Core.Calendar
{
    public class CalendarService : ICalendarService
    {
        private IMenuService _menuService;

        public const int MaxCount = 6;

        public CalendarService( IMenuService menuService )
        {
            _menuService = menuService;
        }

        public Calendar GetCalendar( DateTime date )
        {
            var calendar = new Calendar();

            MenuModel menu = _menuService.GetLastMenu();
            DateTime currentDate = menu != null && date >= DateTime.Today ? menu.Date : date.AddDays( MaxCount - 1 );

            while ( calendar.Items.Count < MaxCount )
            {
                if ( currentDate.DayOfWeek != DayOfWeek.Sunday && currentDate.DayOfWeek != DayOfWeek.Saturday )
                {
                    calendar.Items.Add( currentDate, new CalendarItem( currentDate, _menuService.GetStatus( currentDate ) ) );
                }

                currentDate = currentDate.AddDays( -1 );

                if ( currentDate < DateTime.Today && date >= DateTime.Today )
                {
                    break;
                }
            }

            if ( calendar.Items.Count > 0 )
            {
                calendar.Items = calendar.Items.Reverse().ToDictionary( k => k.Key, k => k.Value );
            }
            else
            {
                calendar.Items.Add( date, new CalendarItem( date, _menuService.GetStatus( date ) ) );
            }

            return calendar;
        }

        public List<DateTime> GetAvailableDates()
        {
            var calendar = GetCalendar( DateTime.Today );
            List<CalendarItem> items = calendar.Items.Values.ToList();

            return items
                .Where( item => item.Status == DayStatus.PreparedForOrder )
                .Select( item => item.Date )
                .ToList();
        }
    }
}
