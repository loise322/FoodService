using System;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Domain.Users
{
    /// <summary>
    /// Принадлежность пользователя к юр. лицу
    /// </summary>
    public class UserLegal
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int LegalId { get; set; }

        public DateTime StartDate { get; set; }

        public TransferReasonsType TransferReason { get; set; }

        public virtual Legal Legal { get;set; }

        public virtual User User { get; set; }
    }
}
