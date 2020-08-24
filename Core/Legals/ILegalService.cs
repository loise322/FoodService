using System.Collections.Generic;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Core.Legals
{
    public interface ILegalService
    {
        Legal GetLegal( int id );

        List<Legal> GetTLLegals();

        List<Legal> GetLegals();

        int SaveLegal( int id, string name, string fullName );

        void RemoveLegal( int id );
    }
}
