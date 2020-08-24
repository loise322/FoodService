using System;
using System.Linq;
using TravelLine.Food.Core;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Users;

namespace TravelLine.Food.Site.Controls
{
    public partial class OrderCalendar : System.Web.UI.UserControl
    {
        private readonly ICalendarService _calendarService;
        private readonly IOrderService _orderService;

        private DateTime _date = DateTime.MinValue;
        protected CalendarItem _currentCalendarDate = null;
        private Calendar _calendar;
        public DateTime SelectedDate
        {
            get { return _date; }
        }

        private UserModel _user = null;
        public UserModel User
        {
            get { return _user; }
            set { _user = value; }
        }

        public CalendarItem CurrentDate
        {
            get { return _currentCalendarDate; }
        }

        public DateTime StartDate
        {
            get
            {
                if ( _calendar != null )
                    return _calendar.Items.First().Key;
                else
                    return DateTime.MinValue;
            }
        }

        public DateTime EndDate
        {
            get
            {
                if ( _calendar != null )
                    return _calendar.Items.Last().Key;
                else
                    return DateTime.MaxValue;
            }
        }

        public OrderCalendar( ICalendarService calendarService, IOrderService orderService )
        {
            _calendarService = calendarService;
            _orderService = orderService;

        }

        protected void Page_Load( object sender, EventArgs e )
        {
            _date = DateTime.Parse( Request.QueryString[ "date" ] ?? DateTime.Today.ToShortDateString() );

            _calendar = _calendarService.GetCalendar( _date );
            if ( _calendar.Items.ContainsKey( _date ) )
            {
                _currentCalendarDate = _calendar.Items[ _date ];
            }
            else
            {
                _currentCalendarDate = new CalendarItem( _date, DayStatus.Closed );
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );

            repDaysList.DataSource = _calendar.Items.Select( i => i.Value );
            repDaysList.DataBind();
        }

        protected string GetStatusStyle( CalendarItem item )
        {
            string res = String.Empty;
            if ( item == null )
            {
                res = "closed";
            }
            else if ( res == String.Empty )
            {
                switch ( item.Status )
                {
                    case DayStatus.Open:
                        res = "opened";
                        break;
                    case DayStatus.PreparedForOrder:
                        res = "order";
                        break;
                    default:
                        res = "closed";
                        break;
                }
            }
            if ( _user != null && res != "closed" )
            {
                if( _user.GetUserLegal( item.Date ) == null )
                {
                    res = "closed";
                }
                else if ( _orderService.IsOrderExist( _user.Id, item.Date ) )
                {
                    res = "ordered";
                }
            }

            if ( item != null && item.Date == _date )
            {
                res += " current active";
            }
            return res;
        }

        protected string GetDayHeaderText( CalendarItem item )
        {
            return "<div><b>" + Library.RusDayOfWeek( item.Date.DayOfWeek ) + " <span class=\"glyphicon glyphicon-ok\" aria-hidden=\"true\"></span></b><br/>" + FormatDate( item ) + "</div>";
        }

        protected string FormatDate( CalendarItem item )
        {
            return String.Format( "{0} {1}", item.Date.Day, Library.RusMonth( item.Date.Month ) );
        }
    }
}
