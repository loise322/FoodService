using System;
using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Orders;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Core.Users
{
    public class UserService : IUserService
    {
        private readonly ILegalService _legalService;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;

        public UserService(
            ILegalService legalService,
            IOrderRepository orderRepository,
            IUserRepository userRepository )
        {
            _legalService = legalService;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public UserModel GetUserByLogin( string login )
        {
            return UserConverter.Convert( _userRepository.GetUserByLogin( login ) );
        }

        public UserModel GetUser( int id )
        {
            if ( id == 0 )
            {
                return new UserModel();
            }

            UserModel model = UserConverter.Convert( _userRepository.Get( id ) );
            if ( model != null && model.UserLegals.Count > 0 )
            {
                model.Legal = _legalService.GetLegal( model.UserLegals[ 0 ].LegalId );
            }

            return model;
        }

        public List<UserModel> GetUsers( int deliveryOfficeId, int legalId, bool onlyActive )
        {
            List<UserModel> users = _userRepository.GetUsers( deliveryOfficeId, legalId ).ConvertAll( UserConverter.Convert );
            List<Legal> legals = _legalService.GetLegals();

            foreach ( UserModel user in users )
            {
                UserLegal userLegal = user.GetUserLegal();
                if ( userLegal != null )
                {
                    user.Legal = legals.FirstOrDefault( l => l.Id == userLegal.LegalId );
                }
            }

            if ( onlyActive )
            {
                users = users.FindAll( u => u.GetUserLegal() != null && u.IsEnabled );
            }
            return users;
        }

        public List<UserModel> GetAllUsers()
        {
            return GetUsers( 0, 0, false );
        }

        public List<UserModel> GetAllEnabledUsers()
        {
            return GetUsers( 0, 0, true );
        }

        public List<UserModel> GetUsersWithoutOrders( DateTime? date )
        {
            List<UserModel> users = GetUsers( 0, 0, date != null );

            List<int> userIdsWithOrders = _orderRepository.GetUsersWithOrders( date );
            var userIdsWithoutOrders = users.Select( u => u.Id ).Except( userIdsWithOrders ).ToList();

            return users.FindAll( user => userIdsWithoutOrders.Contains( user.Id ) );
        }

        public void Save( UserModel model )
        {
            User user = UserConverter.Convert( model );
            _userRepository.Save( user );
            model.Id = user.Id;
        }

        public void DeleteUser( int userId )
        {
            _userRepository.Remove( userId );
        }

        public List<UserModel> Search( string userName )
        {
            return UserConverter.Convert( _userRepository.GetAll() );
        }
    }
}
