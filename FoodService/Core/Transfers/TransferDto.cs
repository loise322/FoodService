using System;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Core.Transfers
{
    public class TransferDto
    {
        public int UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public TransferReasonsType TransferReason { get; set; }

        public int LegalId { get; set; }

        public int UserLegalId { get; set; }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }
    }
}
