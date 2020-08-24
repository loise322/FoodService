using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Domain.DeliveryOffices;

namespace TravelLine.Food.Site
{
    public partial class OrderListRep : System.Web.UI.Page
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly ISupplierService _supplierService;

        public OrderListRep( IDeliveryOfficeService deliveryOfficeService, ISupplierService supplierService )
        {
            _deliveryOfficeService = deliveryOfficeService;
            _supplierService = supplierService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            OrderCalendar.SelectionChanged += new EventHandler( OrderCalendar_SelectionChanged );
            OrderCalendar.DayRender += new DayRenderEventHandler( OrderCalendar_DayRender );
            refreshBtn.Click += new EventHandler( RefreshBtn_Click );
        }

        void OrderCalendar_DayRender( object sender, DayRenderEventArgs e )
        {
            if ( e.Day.Date == OrderListOnFridge.Date )
            {
                e.Cell.BackColor = System.Drawing.Color.Blue;
            }
        }

        void RefreshBtn_Click( object sender, EventArgs e )
        {
            OrderListOnFridge.Date = OrderCalendar.SelectedDate;
            OrderListOnFridge.UsersDeliveryOffice = Convert.ToInt32( ddlDeliveryOffices.SelectedValue );
            OrderListOnFridge.IsStudents = chbStudents.Checked;
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( !IsPostBack )
            {
                List<DeliveryOffice> deliveryOffices = _deliveryOfficeService.GetDeliveryOffices();
                ddlDeliveryOffices.DataSource = deliveryOffices;
                ddlDeliveryOffices.DataBind();

                List<SupplierModel> suppliers = _supplierService.GetSuppliers();
                ddlSuppliers.DataSource = suppliers;
                ddlSuppliers.DataBind();

                OrderListOnFridge.Date = DateTime.Now.Date;
                OrderCalendar.SelectedDate = OrderListOnFridge.Date;
            }
            OrderListOnFridge.UsersDeliveryOffice = Convert.ToInt32( ddlDeliveryOffices.SelectedValue );
            OrderListOnFridge.SupplierId = Convert.ToInt32( ddlSuppliers.SelectedValue );

            LinkB.NavigateUrl = GetFridgeList;
            LinkC.NavigateUrl = GetFridgeList + "&target=excel";
            LinkD.NavigateUrl = GetFridgeList + "&target=word";
        }

        void OrderCalendar_SelectionChanged( object sender, EventArgs e )
        {
            OrderListOnFridge.Date = OrderCalendar.SelectedDate;
            OrderListOnFridge.IsStudents = chbStudents.Checked;
        }

        private string GetFridgeList => String.Format( "./FridgeOrderList.aspx?date={0}&deliveryOffice={1}&supplierId={2}&students={3}",
                        OrderCalendar.SelectedDate.ToShortDateString(),
                        ddlDeliveryOffices.SelectedValue,
                        ddlSuppliers.SelectedValue,
                        chbStudents.Checked.ToString().ToLower() );
    }
}
