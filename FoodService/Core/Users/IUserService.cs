using System;
using System.Collections.Generic;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Core.Users
{
    public interface IUserService
    {
        void DeleteUser( int userId );

        UserModel GetUserByLogin( string login );

        UserModel GetUser( int id );

        List<UserModel> GetAllUsers();

        List<UserModel> GetAllEnabledUsers();

        List<UserModel> GetUsers( int deliveryOfficeId, int legalId, bool showAll );

        List<UserModel> GetUsersWithoutOrders( DateTime? date = null );

        void Save( UserModel user );

        List<UserModel> Search( string userName );
    }
}
