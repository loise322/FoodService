namespace TravelLine.Food.Domain.Users
{
    public interface IUserLegalRepository
    {
        UserLegal GetUserLegal( int id );

        void Save( UserLegal item );

        void Remove( UserLegal item );

        void Remove( int id );
    }
}
