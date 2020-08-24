using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Infrastructure.Common;

namespace TravelLine.Food.Infrastructure.Repositories
{
    public class WorkTimeRepository : EFGenericRepository<WorkTime>, IWorkTimeRepository
    {
        public WorkTimeRepository( DbContext context ) : base( context ) { }

        public WorkTime GetWorkTime( DateTime month, int userId )
        {
            return QueryReadOnly()
                .FirstOrDefault( wt => wt.Month == month && wt.UserId == userId );
        }

        public List<WorkTime> GetWorkTimes( DateTime month )
        {
            return QueryReadOnly()
                .Where( wt => wt.Month == month )
                .ToList();
        }

        public void RemoveWorkTimes( DateTime month )
        {
            var workTimes = Query().Where( wt => wt.Month == month ).ToList();
            if( workTimes.Count > 0 )
            {
                Remove( workTimes );
            }
        }
    }
}
