using System;
using TravelLine.Food.Core;
using TravelLine.Food.Core.DeliveryOffices;

namespace TravelLine.Food.Site.Admin
{
    public partial class FridgeOrderList : System.Web.UI.Page
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;

        private string _date = DateTime.Now.ToShortDateString();
        private int _deliveryOffice;
        private int _supplierId;
        private bool _isStudents = false;

        public FridgeOrderList( IDeliveryOfficeService deliveryOfficeService )
        {
            _deliveryOfficeService = deliveryOfficeService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( Request.QueryString[ "date" ] != null )
            {
                _date = Request.QueryString[ "date" ];
            }

            if ( Request.QueryString[ "deliveryOffice" ] != null )
            {
                _deliveryOffice = Convert.ToInt32( Request.QueryString[ "deliveryOffice" ] );
            }

            if ( Request.QueryString[ "supplierId" ] != null )
            {
                _supplierId = Convert.ToInt32( Request.QueryString[ "supplierId" ] );
            }

            if ( Request.QueryString[ "students" ] != null )
            {
                _isStudents = Boolean.Parse( Request.QueryString[ "students" ] );
            }

            OrderListOnFridge.Date = DateTime.Parse( _date );
            OrderListOnFridge.UsersDeliveryOffice = _deliveryOffice;
            OrderListOnFridge.SupplierId = _supplierId;
            OrderListOnFridge.IsStudents = _isStudents;
        }

        protected override void OnPreRender( EventArgs e )
        {
            base.OnPreRender( e );

            string[] dateArray = _date.Split( '.' );
            _date = String.Empty;
            _date = dateArray[ 2 ] + "_" + dateArray[ 1 ] + "_" + dateArray[ 0 ];

            string fileName = String.Format( "print_{0}_{1}", _date, _deliveryOfficeService.GetDeliveryOfficeName( _deliveryOffice ) )
                .Replace( " ", "_" )
                .Replace( ".", "_" );

            if ( Request.QueryString[ "target" ] == "excel" )
            {
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader( "Content-Disposition", "attachment; filename=" + fileName + ".xls" );
            }
            if ( Request.QueryString[ "target" ] == "word" )
            {
                Response.Buffer = true;
                Response.ContentType = "application/msword";
                Response.AppendHeader( "Content-Disposition", "attachment; filename=" + fileName + ".doc" );
            }
        }
    }
}
