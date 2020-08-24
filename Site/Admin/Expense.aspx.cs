using System;
using System.Collections.Generic;
using System.Text;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.DeliveryOffices;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Site.Admin
{
    public partial class Expense : System.Web.UI.Page
    {
        private DateTime _firstDate = DateTime.MinValue;
        private DateTime _secondDate = DateTime.MaxValue;
        private DateTime _lastDate = DateTime.MinValue;

        private IUserService _userService;
        private IOrderService _orderService;
        private ILegalService _legalService;
        private IDeliveryOfficeService _deliveryOfficeService;

        public Expense(
            IUserService userService,
            IOrderService orderService,
            ILegalService legalService,
            IDeliveryOfficeService deliveryOfficeService )
        {
            _userService = userService;
            _orderService = orderService;
            _legalService = legalService;
            _deliveryOfficeService = deliveryOfficeService;
        }

        public DateTime FirstDate
        {
            get { return _firstDate; }
            set { _firstDate = value; }
        }

        public DateTime SecondDate
        {
            get { return _secondDate; }
            set { _secondDate = value; }
        }

        public struct UserRecord
        {
            public string name;
            public decimal cost;
        }

        public struct Union
        {
            public string name;
            public UserRecord[] userList;
            public decimal totalCost;
        }

        protected void Page_Load( object sender, EventArgs e )
        {

        }

        public void ChangeSelected( object sender, EventArgs e )
        {
            if ( LastDate.Value != "" )
            {
                _lastDate = DateTime.Parse( LastDate.Value );
            }
            GetDates();
            if ( _lastDate == DateTime.MinValue )
            {
                FirstDate = Calendar4Expence.SelectedDate;
            }
            else
            {
                if ( FirstDate == _lastDate )
                {
                    SecondDate = Calendar4Expence.SelectedDate;
                }
                else
                {
                    FirstDate = Calendar4Expence.SelectedDate;
                }
            }
            _lastDate = Calendar4Expence.SelectedDate;
            LastDate.Value = _lastDate.ToShortDateString();

            if ( FirstDate > SecondDate )
            {
                DateTime _temp = FirstDate;
                FirstDate = SecondDate;
                SecondDate = _temp;
            }

            FD.Text = FirstDate.ToShortDateString();
            SD.Text = SecondDate.ToShortDateString();
        }

        private void GetDates()
        {
            if ( ( FD.Text != "" ) && ( SD.Text != "" ) )
            {
                FirstDate = DateTime.Parse( FD.Text );
                SecondDate = DateTime.Parse( SD.Text );
            }
        }

        public Union[] GetAllList()
        {
            GetDates();
            Union[] unions = new Union[ 1 ];

            List<UserModel> allUsers = _userService.GetAllUsers();
            unions[ 0 ].userList = new UserRecord[ allUsers.Count ];
            int usersCount = 0;

            foreach ( UserModel user in allUsers )
            {
                DateTime date1 = FirstDate;
                DateTime date2 = SecondDate;

                unions[ 0 ].userList[ usersCount ].name = user.Name;
                unions[ 0 ].userList[ usersCount ].cost = _orderService.GetUserOrdersSum( date1, date2, user.Id );

                unions[ 0 ].totalCost += unions[ 0 ].userList[ usersCount ].cost;
                usersCount++;
            }

            return unions;
        }

        public Union[] GetLegalList()
        {
            GetDates();

            List<UserModel> allUsers = _userService.GetAllUsers();
            List<Domain.Legals.Legal> legalList = _legalService.GetLegals();
            var unions = new Union[ legalList.Count ];
            int legalCount = 0;
            int userCount = 0;
            DateTime date1 = FirstDate;
            DateTime date2 = SecondDate;
            foreach ( Legal legal in legalList )
            {
                unions[ legalCount ].userList = new UserRecord[ allUsers.Count ];
                unions[ legalCount ].name = legal.Name;
                foreach ( UserModel user in allUsers )
                {
                    if ( user.Legal != null && user.Legal.Id == legal.Id )
                    {
                        unions[ legalCount ].userList[ userCount ].name = user.Name;
                        unions[ legalCount ].userList[ userCount ].cost = _orderService.GetUserOrdersSum( date1, date2, user.Id );

                        unions[ legalCount ].totalCost += unions[ legalCount ].userList[ userCount ].cost;
                        userCount++;
                    }
                    else
                    {
                        continue;
                    }
                }
                userCount = 0;
                legalCount++;
            }

            return unions;
        }

        public Union[] GetDeliveryOfficeList()
        {
            GetDates();
            List<UserModel> allUsers = _userService.GetAllUsers();
            List<DeliveryOffice> deliveryOfficeList = _deliveryOfficeService.GetDeliveryOffices();
            var unions = new Union[ deliveryOfficeList.Count ];
            int deliveryOfficeCount = 0;
            int userCount = 0;
            DateTime date1 = FirstDate;
            DateTime date2 = SecondDate;
            foreach ( DeliveryOffice deliveryOffice in deliveryOfficeList )
            {
                unions[ deliveryOfficeCount ].userList = new UserRecord[ allUsers.Count ];
                unions[ deliveryOfficeCount ].name = deliveryOffice.Name;
                foreach ( UserModel user in allUsers )
                {
                    if ( user.DeliveryOffice.Id == deliveryOffice.Id )
                    {
                        unions[ deliveryOfficeCount ].userList[ userCount ].name = user.Name;
                        unions[ deliveryOfficeCount ].userList[ userCount ].cost += _orderService.GetUserOrdersSum( date1, date2, user.Id );

                        unions[ deliveryOfficeCount ].totalCost += unions[ deliveryOfficeCount ].userList[ userCount ].cost;
                        userCount++;
                    }
                    else
                    {
                        continue;
                    }
                }
                userCount = 0;
                deliveryOfficeCount++;
            }

            return unions;
        }

        public string GetHTMLUnionList( Union[] unions )
        {
            var result = new StringBuilder();
            foreach ( Union deliveryOffice in unions )
            {
                if ( deliveryOffice.totalCost < 1 )
                {
                    continue;
                }

                result.AppendLine( "<h3>" + deliveryOffice.name + "</h3>" );
                result.AppendLine( "<table class=\"table table-bordered table-hover\">" );
                result.AppendLine( "<tr><th>Имя пользователя</th><th class=\"text-right\">Стоимость заказа</th></tr>" );

                foreach ( UserRecord user in deliveryOffice.userList )
                {
                    if ( user.name != null && user.cost > 0 )
                    {
                        result.AppendLine( "<tr><td>" + user.name + "</td><td class=\"text-right\">" + user.cost + "</td></tr>" );
                    }
                }

                result.AppendLine( "<tr><th>Итого:</th><th class=\"text-right\">" + deliveryOffice.totalCost + "</th></tr>" );
                result.AppendLine( "</table>" );
                result.AppendLine( "<br />" );
            }

            return result.ToString();
        }

    }
}
