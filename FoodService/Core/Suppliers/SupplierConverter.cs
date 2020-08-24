using TravelLine.Food.Domain.Suppliers;

namespace TravelLine.Food.Core.Suppliers
{
    internal static class SupplierConverter
    {
        internal static SupplierModel Convert( Supplier supplier )
        {
            return new SupplierModel()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                Address = supplier.Address,
                ContactPerson = supplier.ContactPerson,
                Phone = supplier.Phone,
                Email = supplier.Email,
                LegalEntity = supplier.LegalEntity,
                Discount = supplier.Discount,
                SalatWareCost = supplier.SalatWareCost,
                SoupWareCost = supplier.SoupWareCost,
                SecondWareCost = supplier.SecondWareCost
            };
        }
        internal static Supplier Convert( SupplierModel supplierModel )
        {
            return new Supplier()
            {
                Id = supplierModel.Id,
                Name = supplierModel.Name,
                Address = supplierModel.Address,
                ContactPerson = supplierModel.ContactPerson,
                Phone = supplierModel.Phone,
                Email = supplierModel.Email,
                LegalEntity = supplierModel.LegalEntity,
                Discount = supplierModel.Discount,
                SalatWareCost = supplierModel.SalatWareCost,
                SoupWareCost = supplierModel.SoupWareCost,
                SecondWareCost = supplierModel.SecondWareCost
            };
        }
    }
}
