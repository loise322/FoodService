using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Site.Admin
{
    public partial class DateOrder : System.Web.UI.Page
    {
        private readonly IMenuService _menuService;
        private readonly ISupplierService _supplierService;
        private readonly IDeliveryOfficeService _deliveryOfficeService;

        public DateOrder(
            IMenuService menuService,
            ISupplierService supplierService,
            IDeliveryOfficeService deliveryOfficeService )
        {
            _menuService = menuService;
            _supplierService = supplierService;
            _deliveryOfficeService = deliveryOfficeService;
        }

        public struct StatusOfOrder
        {
            public static string OrderOpen = "Статус заказа: открыт";
            public static string OrderClosed = "Статус заказа: закрыт";
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            DOCalendar.DayRender += new DayRenderEventHandler( DOCalendar_DayRender );
            DOCalendar.DayRender += new DayRenderEventHandler( DOCalendar_DayRender );
        }

        protected void DOCalendar_DayRender( object sender, DayRenderEventArgs e )
        {
            if ( e.Day.Date == DOCalendar.SelectedDate )
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
        }

        protected void Refresh( object sender, EventArgs e )
        {

        }

        private string GetPrintVersion => $"./OrderPrintVersion.aspx?date={DOCalendar.SelectedDate.ToShortDateString()}&deliveryOffice={Convert.ToInt32( ddlDeliveryOffices.SelectedValue )}&supplierId={Convert.ToInt32( ddlSuppliers.SelectedValue )}";

        private void SetOrderStatus( DayStatus status )
        {
            switch ( status )
            {
                case DayStatus.Closed:
                    OrderStatus.Text = StatusOfOrder.OrderClosed;
                    Submit.Visible = false;
                    UnSubmit.Visible = true;
                    break;
                case DayStatus.Open:
                    Submit.Visible = false;
                    UnSubmit.Visible = false;
                    OrderStatus.Text = "";
                    break;
                case DayStatus.PreparedForOrder:
                    OrderStatus.Text = StatusOfOrder.OrderOpen;
                    Submit.Visible = true;
                    UnSubmit.Visible = false;
                    break;

            }
        }

        public void UnSubmitOrder( object sender, EventArgs e )
        {
            MenuModel menu = _menuService.GetMenu( DOCalendar.SelectedDate );
            if ( menu != null )
            {
                _menuService.OpenMenu( menu.Date );
            }
        }

        public void SubmitOrder( object sender, EventArgs e )
        {
            MenuModel menu = _menuService.GetMenu( DOCalendar.SelectedDate );
            if ( menu != null )
            {
                _menuService.CloseMenu( menu.Date );
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( !IsPostBack )
            {
                DOCalendar.SelectedDate = DateTime.Today;
                DateOrderControl.Date = DateTime.Today;

                List<SupplierModel> suppliers = _supplierService.GetSuppliers();
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataBind();

                List<DeliveryOffice> deliveryOffices = _deliveryOfficeService.GetDeliveryOffices();
                ddlDeliveryOffices.DataSource = deliveryOffices;
                ddlDeliveryOffices.DataBind();
            }
            SetOrderStatus( _menuService.GetStatus( DOCalendar.SelectedDate ) );
            DateOrderControl.Date = DOCalendar.SelectedDate;
            DateOrderControl.DeliveryOffice = Convert.ToInt32( ddlDeliveryOffices.SelectedValue ); ;
            DateOrderControl.SupplierId = Convert.ToInt32( ddlSuppliers.SelectedValue );

            LinkB.NavigateUrl = GetPrintVersion;
            LinkC.NavigateUrl = GetPrintVersion + "&target=excel";
            LinkD.NavigateUrl = GetPrintVersion + "&target=word";


        }
    }
}
