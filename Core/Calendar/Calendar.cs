using System;
using System.Collections.Generic;

namespace TravelLine.Food.Core.Calendar
{
    public class Calendar
    {
        public Calendar()
        {
            Items = new Dictionary<DateTime, CalendarItem>();
        }

        public Dictionary<DateTime, CalendarItem> Items { get; set; }

        public CalendarItem GetItem( DateTime date )
        {
            CalendarItem item;

            Items.TryGetValue( date, out item );

            return item;
        }
    }
}
