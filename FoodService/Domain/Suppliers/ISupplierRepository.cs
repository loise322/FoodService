using System.Collections.Generic;

namespace TravelLine.Food.Domain.Suppliers
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAll();

        Supplier Get( int id );

        void Remove( Supplier item );

        void Save( Supplier item );
    }
}
