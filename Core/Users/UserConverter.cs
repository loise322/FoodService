using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Core.Users
{
    internal static class UserConverter
    {
        internal static UserModel Convert( User user )
        {
            if ( user == null )
            {
                return null;
            }

            var result = new UserModel()
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                IsEnabled = user.IsEnabled,
                Code = user.Code,
                ExternalId = user.ExternalId,
                DeliveryOffice = DeliveryOfficeConverter.Convert( user.DeliveryOffice ),
                UserLegals = user.UserLegals.OrderBy( ul => ul.StartDate ).ToList()
            };

            return result;
        }

        internal static User Convert( UserModel model )
        {
            if ( model == null )
            {
                return null;
            }

            var user = new User()
            {
                Id = model.Id,
                Name = model.Name,
                Login = model.Login,
                IsEnabled = model.IsEnabled,
                Code = model.Code,
                ExternalId = model.ExternalId,
                DeliveryOfficeId = model.DeliveryOffice.Id,
                UserLegals = model.UserLegals
            };

            return user;
        }

        internal static List<UserModel> Convert( List<User> users )
        {
            if ( users.Count == 0 )
            {
                return null;
            }
            List<UserModel> result = new List<UserModel>();

            foreach ( User user in users )
            {
                result.Add( Convert( user ) );
            }

            return result;
        }
    }
}
