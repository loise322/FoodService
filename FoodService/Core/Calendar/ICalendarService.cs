using System;
using System.Collections.Generic;

namespace TravelLine.Food.Core.Calendar
{
    public interface ICalendarService
    {
        List<DateTime> GetAvailableDates();

        Calendar GetCalendar( DateTime date );
    }
}
