using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.Orders;

namespace TravelLine.Food.Core.Dishes
{
    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IOrderRepository _orderRepository;

        public DishService( IDishRepository dishRepository, IOrderRepository orderRepository )
        {
            _dishRepository = dishRepository;
            _orderRepository = orderRepository;
        }

        public DishModel GetDish( int id )
        {
            Dish dish = _dishRepository.Get( id );
            if( dish == null )
            {
                return null;
            }

            return DishConverter.Convert( dish );
        }

        public void Save( DishModel model )
        {
            Dish dish = DishConverter.Convert( model );

            _dishRepository.Save( dish );

            if ( model.Id == 0 )
            {
                model.Id = dish.Id;
            }
        }

        public void Remove( int dishId )
        {
            Dish dish = _dishRepository.Get( dishId );
            if( dish != null )
            {
                _dishRepository.Remove( dish );
            }
        }

        public List<DishModel> GetAllDishes()
        {
            return _dishRepository.GetAll()
                .OrderBy( d => d.Name )
                .Select( d => DishConverter.Convert( d ) )
                .ToList();
        }

        public List<DishModel> GetDishesByIds( IEnumerable<int> ids )
        {
            return _dishRepository.Get( ids ).ConvertAll( DishConverter.Convert );
        }

        public List<DishModel> GetDishesByType( DishType type )
        {
            return  _dishRepository.GetByType( type ).ConvertAll( DishConverter.Convert );
        }

        public List<DishModel> GetSupplierDishes( int supplierId )
        {
            return _dishRepository.GetSupplierDishes( supplierId ).ConvertAll( DishConverter.Convert );
        }

        public bool SaveImage( DishModel dish, HttpPostedFile file )
        {
            bool res = true;
            try
            {
                dish.ImagePath = GetImageFileName( file.FileName );
                Bitmap image = new Bitmap( file.InputStream );
                Image smallImage = image.GetThumbnailImage( 90, 70, null, IntPtr.Zero );
                Image bigImage = image.GetThumbnailImage( 256, 144, null, IntPtr.Zero );
                smallImage.Save( ConfigService.ImagesStore + "small/" + dish.ImagePath );
                bigImage.Save( ConfigService.ImagesStore + "big/" + dish.ImagePath );
            }
            catch
            {
                res = false;
            }
            return res;
        }

        public bool IsDishUsed( int id )
        {
            var orders = _orderRepository
                .GetAll()
                .Where( o => o.GarnishId == id || o.SalatId == id || o.SecondDishId == id || o.SoupId == id ).ToList();

            if ( orders.Count > 0 )
            {
                return true;
            }

            return false;
        }

        private static string GetImageFileName( string name )
        {
            string res;
            string[] arrName = name.Split( '.' );
            string extension = arrName[ arrName.Length - 1 ];
            string[] newName = Path.GetRandomFileName().Split( '.' );
            res = String.Format( "{0}.{1}", newName[ 0 ], extension );
            if ( File.Exists( ConfigService.ImagesStore + "big/" + res ) )
            {
                res = GetImageFileName( name );
            }

            return res;
        }
    }
}
