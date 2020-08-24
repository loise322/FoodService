using System;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Domain.WorkTimes
{
    public class WorkTime
    {
        public int Id { get; set; }

        public int LegalId { get; set; }

        public int UserId { get; set; }

        public DateTime Month { get; set; }

        public int Days { get; set; }

        public virtual Legal Legal { get; set; }

        public virtual User User { get; set; }
    }
}
