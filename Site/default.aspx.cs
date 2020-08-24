using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelLine.Food.Core;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Site.Controls;
using TravelLine.Food.Site.Site;

namespace TravelLine.Food.Site
{
    public partial class Default : UserPage
    {
        private readonly ICalendarService _calendarService;
        private readonly IDishService _dishService;
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;

        private OrderModel _order;

        public Default( ICalendarService calendarService, IDishService dishService, IMenuService menuService, IOrderService orderService )
        {
            _calendarService = calendarService;
            _dishService = dishService;
            _menuService = menuService;
            _orderService = orderService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            btnOrder.OnClientClick = String.Format( "return Save('{0}')", OrderResult.ClientID );
            btnOrder.Click += new EventHandler( btnOrder_Click );
        }

        void btnOrder_Click( object sender, EventArgs e )
        {
            if( CurrentUser.GetUserLegal( ocCalendar.SelectedDate ) == null )
            {
                return;
            }

            string res = OrderResult.Value;
            string[] array = res.Split( '|' );
            string[] salats = array[ 0 ].Split( ',' );
            string[] soups = array[ 1 ].Split( ',' );
            string[] second = array[ 2 ].Split( ',' );
            OrderModel order = new OrderModel() { User = CurrentUser, Date = ocCalendar.SelectedDate, Items = new List<OrderItemModel>() };
            int i = 0;
            while ( true )
            {
                DishModel salat = null;
                DishModel soup = null;
                DishModel secondDish = null;
                DishModel garnish = null;

                if ( salats.Length > i )
                {
                    int id = GetDishIdFromString( salats, i );
                    salat = _dishService.GetDish( id );
                }
                if ( soups.Length > i )
                {
                    int id = GetDishIdFromString( soups, i );
                    soup = _dishService.GetDish( id );
                }
                if ( second.Length > i )
                {
                    string[] secondParts = second[ i ].Split( '.' );

                    int id1 = 0;
                    int id2 = 0;

                    if ( secondParts.Length == 2 )
                    {
                        id1 = GetDishIdFromString( secondParts, 0 );
                        id2 = GetDishIdFromString( secondParts, 1 );
                    }

                    secondDish = _dishService.GetDish( id1 );
                    garnish = _dishService.GetDish( id2 );
                }
                if ( salat == null && soup == null && secondDish == null && garnish == null )
                {
                    break;
                }
                OrderItemModel item = new OrderItemModel() { Salat = salat, Soup = soup, SecondDish = secondDish, Garnish = garnish };
                order.Items.Add( item );
                i++;
            }

            _orderService.Save( order );
        }

        private int GetDishIdFromString(string[] str, int index)
        {
            int id = 0;
            int.TryParse( str[ index ], out id );
            return id;
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );

            if ( CurrentUser != null )
            {
                phOrder.Visible = true;
                ocCalendar.User = CurrentUser;
                _order = _orderService.GetOrder( CurrentUser.Id, ocCalendar.SelectedDate );
            }
            else
            {
                phOrder.Visible = false;
            }

            ctlMenuSelector.Menu = _menuService.GetMenu( ocCalendar.SelectedDate );
            phMenu.Visible = ctlMenuSelector.Menu != null ? !ctlMenuSelector.Menu.IsOrdered : false;
            if ( ocCalendar.CurrentDate == null || ocCalendar.CurrentDate.Status != DayStatus.PreparedForOrder || CurrentUser?.GetUserLegal( ocCalendar.SelectedDate ) == null )
            {
                ctlMenuSelector.Display = false;
                btnOrder.Visible = false;
                phMenu.Visible = false;
            }
            else
            {
                ctlMenuSelector.Display = true;
                btnOrder.Visible = true;
            }

            if ( ocCalendar.CurrentDate.Status == DayStatus.PreparedForOrder )
            {
                if ( _order != null )
                {
                    btnOrder.CssClass = "btn btn-success";
                    btnOrder.Text = "Перезаказать";
                }
                else
                {
                    btnOrder.CssClass = "btn btn-danger";
                    btnOrder.Text = "Заказать";
                }
            }

            if ( !IsPostBack )
            {
                var menu = Master.FindControl( "MainMenu" ) as MainMenu;
                if ( menu != null )
                {
                    menu.ShowTotalSum();
                }
            }

            if ( CurrentUser != null )
            {
                var daysWithoutOrded = new List<DateTime>();
                Calendar calendar = _calendarService.GetCalendar( ocCalendar.SelectedDate );
                
                foreach ( KeyValuePair<DateTime, CalendarItem> item in calendar.Items.Where( x => x.Value.Status == DayStatus.PreparedForOrder ) )
                {
                    if ( !_orderService.IsOrderExist( CurrentUser.Id, item.Key ) && CurrentUser.GetUserLegal( item.Key ) != null )
                    {
                        daysWithoutOrded.Add( item.Key );
                    }
                }
                if ( daysWithoutOrded.Count > 0 )
                {
                    lblOrderStatus.Text = String.Format( "<span style='color: red;'>Закажите обед на {0}!</span>",
                        String.Join( ", ", daysWithoutOrded.Select( x => CreateDateText( x ) ).ToArray() ) );
                }
                else
                {
                    lblOrderStatus.Text = String.Empty;
                }
            }
        }

        private string CreateDateText( DateTime date )
        {
            string dateLink = System.Web.VirtualPathUtility.ToAbsolute( Library.GetDateSelectionLink( date ) );
            return string.Format( "<a style='color: red; text-decoration: underline;' href='{0}'>{1}</a>", Server.HtmlEncode( dateLink ), Server.HtmlEncode( date.ToString( "dd MMMM" ) ) );
        }

        protected string OrderedList()
        {
            StringBuilder res = new StringBuilder();
            if ( _order != null )
            {
                if ( _order.Items.Count > 0 )
                {
                    foreach ( OrderItemModel i in _order.Items )
                    {
                        if ( i.Salat != null )
                            res.Append( String.Format( "addDishToOrderLoad({0});", i.Salat.Id ) );
                        if ( i.Garnish != null )
                            res.Append( String.Format( "addDishToOrderLoad({0});", i.Garnish.Id ) );
                        if ( i.SecondDish != null )
                            res.Append( String.Format( "addDishToOrderLoad({0});", i.SecondDish.Id ) );
                        if ( i.Soup != null )
                            res.Append( String.Format( "addDishToOrderLoad({0});", i.Soup.Id ) );
                    }
                }
            }
            return res.ToString();
        }
    }
}
