using System;
using System.Collections.Generic;

namespace TravelLine.Food.Domain.WorkTimes
{
    public interface IWorkTimeRepository
    {
        List<WorkTime> GetAll();

        WorkTime Get( int id );

        WorkTime GetWorkTime( DateTime month, int userId );

        List<WorkTime> GetWorkTimes( DateTime month );

        void RemoveWorkTimes( DateTime month );

        void Save( WorkTime item );

        void Save( List<WorkTime> items );
    }
}
