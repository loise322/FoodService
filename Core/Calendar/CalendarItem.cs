using System;
using TravelLine.Food.Core.Dishes;

namespace TravelLine.Food.Core.Calendar
{
    public class CalendarItem
    {
        public DateTime Date { get; private set; }

        public DayStatus Status { get; private set; }

        public string WeekDay => Library.RusDayOfWeek( Date.DayOfWeek );

        public CalendarItem( DateTime date, DayStatus status )
        {
            Date = date;
            Status = status;
        }
    }
}
