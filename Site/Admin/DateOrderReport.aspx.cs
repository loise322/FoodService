using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.Legals;

namespace TravelLine.Food.Site.Admin
{
    public partial class DateOrderReport : System.Web.UI.Page
    {
        private readonly IMenuService _menuService;
        private readonly ILegalService _legalService;
        private readonly ISupplierService _supplierService;

        public struct StatusOfOrder
        {
            public static string OrderOpen = "Статус заказа: открыт";
            public static string OrderClosed = "Статус заказа: закрыт";
        }

        public DateOrderReport(
            IMenuService menuService,
            ILegalService legalService,
            ISupplierService supplierService )
        {
            _menuService = menuService;
            _legalService = legalService;
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            DOCalendar.DayRender += new DayRenderEventHandler( DOCalendar_DayRender );
        }

        protected void DOCalendar_DayRender( object sender, DayRenderEventArgs e )
        {
            if ( e.Day.Date == DOCalendar.SelectedDate )
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
        }

        private string GetPrintLink( string target = null )
        {
            string url = $"./OrderPrintVersion.aspx?date={DOCalendar.SelectedDate.ToShortDateString()}&legal={ddlLegals.SelectedValue}&supplierId={ddlSuppliers.SelectedValue}";
            if ( !String.IsNullOrEmpty( target ) )
            {
                url += "&target=" + target;
            }

            return url;
        }

        private void SetOrderStatus( DayStatus status )
        {
            switch ( status )
            {
                case DayStatus.Closed:
                    OrderStatus.Text = StatusOfOrder.OrderClosed;
                    break;
                case DayStatus.Open:
                    OrderStatus.Text = "";
                    break;
                case DayStatus.PreparedForOrder:
                    OrderStatus.Text = StatusOfOrder.OrderOpen;
                    break;
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( !IsPostBack )
            {
                DOCalendar.SelectedDate = DateTime.Today;
                DateOrderControl.Date = DateTime.Today;

                List<Legal> legals = _legalService.GetTLLegals();
                legals.Add( new Legal() { Id = 0, FullName = "Прочие" } );

                ddlLegals.DataSource = legals;
                ddlLegals.DataBind();

                List<SupplierModel> suppliers = _supplierService.GetSuppliers();
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataBind();

            }
            SetOrderStatus( _menuService.GetStatus( DOCalendar.SelectedDate ) );
            DateOrderControl.Date = DOCalendar.SelectedDate;
            DateOrderControl.Legal = Int32.Parse( ddlLegals.SelectedValue );
            if ( DateOrderControl.Legal == 0 )
            {
                DateOrderControl.ExcludedLegals = ConfigService.TravellineLegals;
            }
            DateOrderControl.SupplierId = Int32.Parse( ddlSuppliers.SelectedValue );

            lnkPrint.NavigateUrl = GetPrintLink();
            lnkExportExcel.NavigateUrl = GetPrintLink( "excel" );
            lnkExportWord.NavigateUrl = GetPrintLink( "word" );
        }

        protected void Refresh( object sender, EventArgs e )
        {

        }
    }
}
