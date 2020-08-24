using System;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Legals;

namespace TravelLine.Food.Site.Admin
{
    public partial class OrderPrintVersion : System.Web.UI.Page
    {
        private readonly IDeliveryOfficeService _deliveryOfficeService;
        private readonly ILegalService _legalService;

        private string _date = DateTime.Now.ToShortDateString();
        private int _deliveryOffice;
        private int? _legal = null;
        private int _supplierId;

        public OrderPrintVersion(
            IDeliveryOfficeService DeliveryOfficeService,
            ILegalService LegalService )
        {
            _deliveryOfficeService = DeliveryOfficeService;
            _legalService = LegalService;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( Request.QueryString[ "date" ] != null )
            {
                _date = Request.QueryString[ "date" ];
            }

            if ( Request.QueryString[ "deliveryOffice" ] != null )
            {
                _deliveryOffice = Int32.Parse( Request.QueryString[ "deliveryOffice" ] );
            }

            if ( Request.QueryString[ "legal" ] != null )
            {
                _legal = Int32.Parse( Request.QueryString[ "legal" ] );
            }

            if ( Request.QueryString[ "supplierId" ] != null )
            {
                _supplierId = Int32.Parse( Request.QueryString[ "supplierId" ] );
            }

            dateOrder.Date = DateTime.Parse( _date );
            dateOrder.SupplierId = _supplierId;

            if ( _legal != null )
            {
                dateOrder.Legal = _legal.Value;
                if ( dateOrder.Legal == 0 )
                {
                    dateOrder.ExcludedLegals = ConfigService.TravellineLegals;
                }
            }
            else
            {
                dateOrder.DeliveryOffice = _deliveryOffice;
            }
        }

        protected override void OnPreRender( EventArgs e )
        {
            string name = String.Empty;
            if ( _legal != null )
            {
                name = _legal.Value != 0 ? _legalService.GetLegal( _legal.Value ).Name : "Прочие";
            }
            else
            {
                string[] dateArray = _date.Split( '.' );
                _date = String.Empty;
                _date = dateArray[ 2 ] + "_" + dateArray[ 1 ] + "_" + dateArray[ 0 ];

                name = _deliveryOfficeService.GetDeliveryOfficeName( _deliveryOffice );
            }

            string fileName = "order_" + _date + "_" + name;
            fileName = fileName.Replace( " ", "_" );
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
