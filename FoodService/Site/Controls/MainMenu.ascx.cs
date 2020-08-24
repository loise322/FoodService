using System;
using System.Linq;
using System.Web;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Site.Site;

namespace TravelLine.Food.Site.Controls
{
    public partial class MainMenu : UserControl
    {
        private readonly ICalendarService _calendarService;
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly IUserService _userService;

        public MainMenu( ICalendarService calendarService, IDeliveryOfficeService deliveryOfficeService, IUserService userService )
        {
            _calendarService = calendarService;
            _deliveryOfficeService = deliveryOfficeService;
            _userService = userService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                IOrderedEnumerable<UserModel> users = _userService.GetAllEnabledUsers().OrderBy( u => u.Name );
                lbUserList.DataSource = users;
                lbUserList.DataValueField = "Id";
                lbUserList.DataTextField = "Name";
                lbUserList.DataBind();

                if ( CurrentUser != null && CurrentUser.GetUserLegal() != null )
                {
                    lbUserList.Items.FindByValue( CurrentUser.Id.ToString() ).Selected = true;
                }
                else
                {
                    lbUserList.Items.Insert( 0, new System.Web.UI.WebControls.ListItem( "Выберите пользователя", "0" ) );
                    lbUserList.SelectedIndex = 0;
                }
            }
        }

        public bool IsMainPage { get; set; }
        public bool IsAdminPage { get; set; }

        public void AddItem( String text, String url, string target = null )
        {
            if ( !String.IsNullOrEmpty( target ) )
            {
                target = String.Format( "target=\"{0}\"", target );
            }
        }

        public void ShowTotalSum()
        {
            totalSum.Visible = true;
        }

        private void SetSelectedUser( UserModel user )
        {
            var cookie = new HttpCookie( ConfigService.CookieUserID, user.Id.ToString() );
            cookie.Expires = DateTime.MaxValue;
            Response.Cookies.Add( cookie );
        }

        protected void UpdateUser( object sender, EventArgs args )
        {
            var dlList = ( System.Web.UI.WebControls.DropDownList )sender;
            int id_user = Convert.ToInt32( dlList.Items[ dlList.SelectedIndex ].Value );
            UserModel user = _userService.GetUser( id_user );
            if ( user != null )
            {
                SetSelectedUser( user );
            }

            Response.Redirect( Request.RawUrl );
        }
    }
}
