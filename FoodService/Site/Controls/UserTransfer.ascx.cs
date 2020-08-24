using System;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Site.Site;

namespace TravelLine.Food.Site.Controls
{
    public partial class UserTransfer : UserControl
    {
        public UserModel User { get; set; }
        private readonly IUserService _userService;
        private readonly ILegalService _legalService;

        public UserTransfer( IUserService userService, ILegalService legalService )
        {
            _userService = userService;
            _legalService = legalService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            lbLegalId.DataSource = _legalService.GetLegals();
            lbLegalId.DataValueField = "Id";
            lbLegalId.DataTextField = "Name";
            lbLegalId.DataBind();
        }
    }
}
