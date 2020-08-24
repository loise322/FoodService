using System.Collections.Generic;

namespace TravelLine.Food.Domain.Users
{
    public interface IUserRepository
    {
        User Get( int id );

        List<User> GetAll();

        User GetUserByLogin( string login );

        List<User> GetUsers( int deliveryOfficeID, int legalID );

        void Save( User item );

        void Remove( int id );
    }
}
