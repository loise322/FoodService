using System;
using System.Data;
using System.Linq;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Reports;
using TravelLine.Food.Core.Suppliers;

namespace TravelLine.Food.Site.Controls
{
    public partial class DateOrder : System.Web.UI.UserControl
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly IReportService _reportService;
        private readonly ILegalService _legalService;
        private readonly ISupplierService _supplierService;

        public DateOrder(
            IDeliveryOfficeService deliveryOfficeService,
            IReportService reportService,
            ILegalService legalService,
            ISupplierService supplierService )
        {
            _deliveryOfficeService = deliveryOfficeService;
            _reportService = reportService;
            _legalService = legalService;
            _supplierService = supplierService;
        }

        public DateTime Date { get; set; }

        public int DeliveryOffice { get; set; }

        public int Legal { get; set; }

        public int[] ExcludedLegals { get; set; }

        public int SupplierId { get; set; }

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( HiddeDate.Value != "" )
            {
                Date = DateTime.Parse( HiddeDate.Value );
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );

            DOPH.Visible = false;
            if ( Date != DateTime.MinValue )
            {
                DOPH.Visible = true;
                DataTable oDT = _reportService.GetRestaurantList( Date, DeliveryOffice, Legal, ExcludedLegals, SupplierId );
                OrderGrid.DataSource = oDT;
                OrderGrid.DataBind();
                SetTotalCost( oDT );
            }

            SupplierModel supplier = _supplierService.GetSupplier( SupplierId );
            lblRestaurant.Text = supplier.LegalEntity;
            if ( DeliveryOffice > 0 )
            {
                lblDeliveryOffice.Text = _deliveryOfficeService.GetDeliveryOfficeName( DeliveryOffice );
                lblTitle.Text = "Накладная N .....";
            }
            else
            {
                lblDeliveryOffice.Text = Legal != 0 ? _legalService.GetLegal( Legal ).FullName : "Прочие";
                lblTitle.Text = "Заказ";
            }
            dateDisp.Text = Date.ToLongDateString();
        }

        private void SetTotalCost( DataTable oDT )
        {
            decimal res = 0;
            foreach ( DataRow row in oDT.Rows )
            {
                res += Convert.ToDecimal( row[ "Sum" ] );
            }
            TotalLabel.Text = res.ToString();
        }
    }
}
