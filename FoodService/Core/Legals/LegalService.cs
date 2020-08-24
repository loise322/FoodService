using System.Collections.Generic;
using System.Linq;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Core.Legals
{
    public class LegalService : ILegalService
    {
        public const int DefaultLegal = 1;

        private readonly ILegalRepository _legalRepository;
        private List<Legal> _legals;

        public LegalService( ILegalRepository legalRepository )
        {
            _legalRepository = legalRepository;
        }

        public Legal GetLegal( int id )
        {
            return _legalRepository.Get( id );
        }

        public List<Legal> GetTLLegals()
        {
            return GetLegals()
                .Where( x => ConfigService.TravellineLegals.Contains( x.Id ) )
                .ToList();
        }

        public List<Legal> GetLegals()
        {
            if( _legals == null )
            {
                _legals = _legalRepository.GetAll().FindAll( l => !l.IsDeleted );
            }

            return _legals;
        }

        public int SaveLegal( int id, string name, string fullName )
        {
            var legal = new Legal()
            {
                Id = id,
                FullName = fullName,
                Name = name
            };

            _legalRepository.Save( legal );

            _legals = null;

            return legal.Id;
        }

        public void RemoveLegal( int id )
        {
            var legal = _legalRepository.Get( id );
            if ( legal != null )
            {
                legal.IsDeleted = true;

                _legalRepository.Save( legal );

                _legals = null;
            }
        }
    }
}
