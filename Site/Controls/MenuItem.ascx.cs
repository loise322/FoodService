using System;
using System.Text;
using System.Text.RegularExpressions;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishRatings;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Site.Site;

namespace TravelLine.Food.Site.Controls
{
    public partial class MenuItem : UserControl
    {
        private readonly IDishRatingService _dishRatingService;
        private readonly ISupplierService _supplierService;

        public DishModel Dish { get; set; }

        public int AvailableQuota { get; set; }

        public MenuItem(
            IDishRatingService dishRatingService,
            ISupplierService supplierService )
        {
            _dishRatingService = dishRatingService;
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected string IsUserLiked()
        {
            if ( Dish.UserIsLiked != null )
            {
                return Dish.UserIsLiked.Value.ToString().ToLower();
            }

            return String.Empty;
        }

        protected string GetLikes()
        {
            if ( Dish.Rating == null )
            {
                Dish.Rating = _dishRatingService.GetDishRating( Dish.Id );
                Dish.UserIsLiked = CurrentUser != null ? _dishRatingService.GetUserIsLiked( Dish.Id, CurrentUser.Id ) : null;
            }

            return Dish.Rating.Likes.ToString();
        }

        protected string GetDislikes()
        {
            if ( Dish.Rating == null )
            {
                return String.Empty;
            }

            return Dish.Rating.Dislikes.ToString();
        }

        protected string GetSupplierName()
        {
            if ( Dish.SupplierId == 0 )
            {
                return String.Empty;
            }

            return _supplierService.GetSupplier( Dish.SupplierId ).Name;
        }

        protected decimal GetDishCost()
        {
            if ( Dish.SupplierId != 0 )
            {
                if ( Dish.Type == Domain.Dishes.DishType.Salat )
                {
                    return Dish.Cost + Dish.Supplier.SalatWareCost;
                }

                if ( Dish.Type == Domain.Dishes.DishType.Soup )
                {
                    return Dish.Cost + Dish.Supplier.SoupWareCost;
                }

                if ( Dish.Type == Domain.Dishes.DishType.SecondDish )
                {
                    return Dish.Cost + Dish.Supplier.SecondWareCost;
                }
            }

            return Dish.Cost;
        }

        protected string GetDishInString()
        {
            var sb = new StringBuilder();

            sb.Append( "\"" );
            sb.Append( Dish.Id );
            sb.Append( "\"," );
            sb.Append( "\"" );
            sb.Append( Regex.Escape( Dish.Name ).Replace( "\"", "\\\"" ) );
            sb.Append( "\"," );
            sb.Append( "\"" );

            if ( Dish.ImagePath != null && Dish.ImagePath.Length > 0 )
            {
                sb.Append( Regex.Escape( ConfigService.WebImagesStoreBig ) );
                sb.Append( Dish.ImagePath );
            }
            sb.Append( "\"," );
            sb.Append( "\"" );
            if ( Dish.ImagePath != null && Dish.ImagePath.Length > 0 )
            {
                sb.Append( Regex.Escape( ConfigService.WebImagesStoreSmall ) );
                sb.Append( Dish.ImagePath );
            }
            sb.Append( "\"," );
            sb.Append( "\"" );
            sb.Append( GetDishCost() );
            sb.Append( "\"," );
            sb.Append( "\"" );
            if ( !String.IsNullOrEmpty( Dish.Description ) )
            {
                sb.Append( Regex.Escape( Dish.Description ).Replace( "\"", "\\\"" ) );
            }
            sb.Append( "\"," );
            sb.Append( "\"" );
            sb.Append( Dish.IsSingle );
            sb.Append( "\"," );
            sb.Append( "\"" );
            sb.Append( ( int )Dish.Type );
            sb.Append( "\"," );
            sb.Append( "\"" );
            sb.Append( Dish.Weight );
            sb.Append( "\"," );
            sb.Append( "\"" );
            sb.Append( Dish.SupplierId );
            sb.Append( "\"" );

            return sb.ToString();
        }
    }
}
