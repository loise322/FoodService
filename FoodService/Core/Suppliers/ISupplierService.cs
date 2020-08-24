using System.Collections.Generic;

namespace TravelLine.Food.Core.Suppliers
{
    public interface ISupplierService
    {
        SupplierModel GetSupplier( int id );

        List<SupplierModel> GetSuppliers();

        void SaveSupplier( SupplierModel supplierModel );

        void RemoveSupplier( int id );
    }
}
