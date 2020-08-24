using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Site.Site;

namespace TravelLine.Food.Site
{
    public partial class Account : UserPage
    {
        private readonly IOrderService _orderService;

        public Account( IOrderService orderService )
        {
            _orderService = orderService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            if( CurrentUser == null )
            {
                Response.Redirect( "~/" );
            }

            var order = _orderService.GetOrder( CurrentUser.Id, DateTime.Today );
            if (order != null)
            {
                salatOrder.DataSource = order.Items.Select( x => x.Salat ).Where( x => x != null ).ToList();
                salatOrder.DataBind();

                soupOrder.DataSource = order.Items.Select( x => x.Soup ).Where( x => x != null ).ToList();
                soupOrder.DataBind();

                secondDishOrder.DataSource = order.Items.Select( x => x.SecondDish ).Where( x => x != null ).ToList();
                secondDishOrder.DataBind();

                garnishOrder.DataSource = order.Items.Select( x => x.Garnish ).Where( x => x != null ).ToList();
                garnishOrder.DataBind();
            }

            if ( !IsPostBack )
            {
                var year = DateTime.Today.Year;
                for ( var i = year; i >= year - 3; i-- )
                {
                    ddlYear.Items.Add( new ListItem() { Text = i.ToString(), Value = i.ToString() } );
                }

                ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
                ddlYear.SelectedValue = DateTime.Today.Year.ToString();
            }
            else
            {
                TabName.Value = Request.Form[ TabName.UniqueID ];
            }
        }

        private void Calculate()
        {
            int year = Int32.Parse( ddlYear.SelectedValue );
            int month = Int32.Parse( ddlMonth.SelectedValue );

            var startDate = new DateTime( year, month, 1 );
            var endDate = new DateTime( year, month, DateTime.DaysInMonth( year, month ) );

            System.Collections.Generic.List<UserOrderPriceModel> orders = _orderService.GetUserOrders( startDate, endDate, CurrentUser.Id );

            reportOrders.DataSource = orders;
            reportOrders.DataBind();

            decimal total = orders.Sum(x => x.Total);
            int days = orders.Count;
            decimal deposit = (days * ConfigService.GetUserDayPriceQuota( startDate )) - total;
            decimal overquota = deposit > 0 ? 0 : deposit * -1;

            lblTotalCost.Text = orders.Sum( x => x.Cost ).ToString( "N" );
            lblTotalDishesCost.Text = orders.Sum( x => x.DishesCost ).ToString( "N" );
            lblTotal.Text = total.ToString( "N" );
            lblDays.Text = String.Format( "за {0} заказов:", days );
            lblOverquota.Text = overquota.ToString( "N" );
            lblHackerSum.Text = deposit.ToString( "N" );
        }

        protected void btnReport_Click( object sender, EventArgs e )
        {
            Calculate();
            tblReport.Visible = true;
        }
    }
}
