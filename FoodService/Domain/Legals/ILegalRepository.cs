using System.Collections.Generic;

namespace TravelLine.Food.Domain.Legals
{
    public interface ILegalRepository
    {
        List<Legal> GetAll();

        Legal Get( int id );

        void Remove( Legal item );

        void Save( Legal item );
    }
}
