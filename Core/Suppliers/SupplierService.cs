using System.Collections.Generic;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Suppliers;

namespace TravelLine.Food.Core.Legals
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService( ISupplierRepository supplierRepository )
        {
            _supplierRepository = supplierRepository;
        }

        public SupplierModel GetSupplier( int id )
        {
            if ( id == 0 )
            {
                return new SupplierModel();
            }

            return SupplierConverter.Convert( _supplierRepository.Get( id ) );
        }

        public List<SupplierModel> GetSuppliers()
        {
            return _supplierRepository.GetAll().ConvertAll( SupplierConverter.Convert );
        }

        public void SaveSupplier( SupplierModel supplierModel )
        {
            Supplier supplier = SupplierConverter.Convert( supplierModel );
            _supplierRepository.Save( supplier );
            supplierModel.Id = supplier.Id;
        }

        public void RemoveSupplier( int id )
        {
            Supplier supplier = _supplierRepository.Get( id );
            _supplierRepository.Remove( supplier );
        }
    }
}
