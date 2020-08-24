using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Core.Users
{
    public class UserModel
    {
        public UserModel()
        {
            DeliveryOffice = new DeliveryOfficeModel();
            UserLegals = new List<UserLegal>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public bool IsEnabled { get; set; }

        public string Code { get; set; }

        public string ExternalId { get; set; }

        public TransferReasonsType TransferReason { get; set; }

        public DeliveryOfficeModel DeliveryOffice { get; set; }

        public Legal Legal { get; set; }

        public List<UserLegal> UserLegals { get; set; }

        public UserLegal GetUserLegal( DateTime? date = null )
        {
            if ( date == null ) date = DateTime.Today;

            UserLegal userLegal = UserLegals.LastOrDefault( ul => ul.StartDate <= date );
            if ( userLegal != null )
            {
                if ( userLegal.TransferReason != TransferReasonsType.Dismissal )
                {
                    return userLegal;
                }

                if ( userLegal.StartDate == date && userLegal.TransferReason == TransferReasonsType.Dismissal )
                {
                    return userLegal;
                }
            }
            return null;
        }
    }
}
