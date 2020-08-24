using System;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishQuotas;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Site.Site;

namespace TravelLine.Food.Site.Controls
{
    public partial class MenuSelector : UserControl
    {
        private readonly IDishQuotaService _dishQuotaService;

        public MenuModel Menu { get; set; }
        public bool Display { get; set; }

        public MenuSelector( IDishQuotaService dishQuotaService )
        {
            _dishQuotaService = dishQuotaService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {

        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( Menu == null )
            {
                return;
            }

            Menu.User = CurrentUser;
            if ( Menu.User == null )
            {
                return;
            }

            repSalat.DataSource = Menu.GetByDishType( DishType.Salat );
            repSalat.DataBind();

            repSecondDish.DataSource = Menu.GetByDishType( DishType.SecondDish );
            repSecondDish.DataBind();

            repSoup.DataSource = Menu.GetByDishType( DishType.Soup );
            repSoup.DataBind();

            repGarnish.DataSource = Menu.GetByDishType( DishType.Garnish );
            repGarnish.DataBind();
        }

        protected string GetImgUrl( DishModel dish )
        {
            return Response.ApplyAppPathModifier( dish.ImagePath );
        }

        protected int GetAvailableQuota( DishModel dish )
        {
            return _dishQuotaService.GetAvailableDishQuota( CurrentUser.Id, Menu.Date, dish );
        }
    }
}
