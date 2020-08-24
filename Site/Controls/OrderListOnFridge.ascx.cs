using System;
using System.Web.UI.WebControls;
using TravelLine.Food.Core;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Reports;

namespace TravelLine.Food.Site.Controls
{
    public partial class OrderListOnFridge : System.Web.UI.UserControl
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly IReportService _reportService;

        public OrderListOnFridge( IDeliveryOfficeService deliveryOfficeService, IReportService reportService )
        {
            _deliveryOfficeService = deliveryOfficeService;
            _reportService = reportService;
        }

        private string _lastName = "";

        private System.Drawing.Color _lastColor;
        private readonly System.Drawing.Color Color1 = System.Drawing.Color.FromArgb( 255, 255, 255 );
        private readonly System.Drawing.Color Color2 = System.Drawing.Color.FromArgb( 153, 204, 255 );

        public DateTime Date { get; set; }

        public int UsersDeliveryOffice { get; set; }

        public int SupplierId { get; set; }

        public bool IsStudents { get; set; }

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( HiddenDate.Value != "" )
            {
                Date = DateTime.Parse( HiddenDate.Value );
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );
            if ( Date != DateTime.MinValue )
            {
                OrderGrid.DataSource = _reportService.OrderedList( Date, UsersDeliveryOffice, SupplierId, IsStudents );
                OrderGrid.RowCreated += new GridViewRowEventHandler( OrderGrid_RowCreated );
                OrderGrid.DataBind();

                HiddenDate.Value = Date.ToShortDateString();
                dateDisp.Text = String.Format( "{0} {1}", Date.ToLongDateString(), Library.RusDayOfWeek( Date.DayOfWeek ) );
            }
            lblDeliveryOffice.Text = _deliveryOfficeService.GetDeliveryOfficeName( UsersDeliveryOffice );
            if ( IsStudents )
            {
                lblLegal.Text = "Студенты";
            }
        }

        private void OrderGrid_RowCreated( object sender, GridViewRowEventArgs e )
        {
            e.Row.DataBind();
            if ( _lastName == "" )
            {
                e.Row.BackColor = Color1;
                _lastColor = Color1;
                _lastName = e.Row.Cells[ 0 ].Text;
                return;
            }
            e.Row.BackColor = _lastColor;
            if ( e.Row.Cells[ 0 ].Text != _lastName )
            {
                ChangeRowColor( e.Row );
                _lastName = e.Row.Cells[ 0 ].Text;
            }
        }

        private void ChangeRowColor( GridViewRow row )
        {
            if ( row.BackColor == Color1 )
            {
                row.BackColor = Color2;
            }
            else
            {
                row.BackColor = Color1;
            }
            _lastColor = row.BackColor;
        }
    }
}
