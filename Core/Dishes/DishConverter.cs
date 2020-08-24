using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Core.Dishes
{
    internal static class DishConverter
    {
        public static Dish Convert( DishModel model )
        {
            return new Dish()
            {
                Cost = model.Cost,
                Description = model.Description,
                Id = model.Id,
                ImagePath = model.ImagePath,
                Name = model.Name,
                IsSingle = model.IsSingle,
                Type = model.Type,
                Weight = model.Weight,
                SupplierId = model.SupplierId
            };
        }

        public static DishModel Convert( Dish model )
        {
            if ( model == null )
            {
                return null;
            }

            return new DishModel()
            {
                Cost = model.Cost,
                Description = model.Description,
                Id = model.Id,
                ImagePath = model.ImagePath,
                Name = model.Name,
                IsSingle = model.IsSingle,
                Type = model.Type,
                Weight = model.Weight,
                SupplierId = model.SupplierId,
                Supplier = model.Supplier != null ? SupplierConverter.Convert( model.Supplier ) : null
            };
        }

        public static DishModel Convert( Dish dish, decimal? cost = null )
        {
            DishModel model = Convert( dish );
            if( model != null && cost != null && cost != 0 )
            {
                model.Cost = cost.Value;
            }

            return model;
        }
    }
}
