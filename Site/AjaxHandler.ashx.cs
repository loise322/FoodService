using System;
using System.Net;
using System.Web;
using TravelLine.Food.Core.DishRatings;

namespace TravelLine.Food.Site
{
    /// <summary>
    /// Summary description for AjaxHandler
    /// </summary>
    public class AjaxHandler : IHttpHandler
    {
        private readonly IDishRatingService _dishRatingService;

        public AjaxHandler( IDishRatingService dishRatingService )
        {
            _dishRatingService = dishRatingService;
        }

        public void ProcessRequest( HttpContext context )
        {
            switch( context.Request.QueryString["action"] )
            {
                case "rating":
                    SetDishRating( context );
                    break;
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
            }
        }

        private void SetDishRating( HttpContext context )
        {
            if( context.Request.RequestType != "POST" )
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            int dishId = Int32.Parse( context.Request.Form[ "dishId" ] );
            int userId = Int32.Parse( context.Request.Form[ "userId" ] );
            bool isLike = Boolean.Parse( context.Request.Form[ "isLike" ] );

            _dishRatingService.SetDishRating( dishId, userId, isLike );

            DishRatingModel rating = _dishRatingService.GetDishRating( dishId );

            context.Response.Write( rating.ToString() + ',' + _dishRatingService.GetUserIsLiked( dishId, userId ).ToString().ToLower() );
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
