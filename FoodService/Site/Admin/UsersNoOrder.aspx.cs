using System;
using System.Collections.Generic;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Users;

namespace TravelLine.Food.Site.Admin
{
    public partial class UsersNoOrder : System.Web.UI.Page
    {
        private readonly IUserService _userService;

        private List<UserModel> _users = new List<UserModel>();
        private DateTime _date = DateTime.Now;

        public UsersNoOrder( IUserService userService )
        {
            _userService = userService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
        }

        protected override void OnPreRender( EventArgs e )
        {
            if ( !IsPostBack )
            {
                _date = DateTime.Today;
                clDate.SelectedDate = _date.Date;
            }
            else
            {
                _date = clDate.SelectedDate;
            }
            _users = _userService.GetUsersWithoutOrders( _date );

            base.OnPreRender( e );
            gwUsersList.DataSource = _users;
            gwUsersList.DataBind();
        }
    }
}
