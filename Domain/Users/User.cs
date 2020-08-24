using System.Collections.Generic;
using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Domain.Users
{
    /// <summary>
    /// Пользователь (Сотрудник)
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int DeliveryOfficeId { get; set; }

        public string Login { get; set; }

        public bool IsEnabled { get; set; }

        public string Code { get; set; }

        public string ExternalId { get; set; }

        public virtual DeliveryOffice DeliveryOffice { get; set; }

        public virtual ICollection<UserLegal> UserLegals { get; set; }
    }
}
