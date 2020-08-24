using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Reports;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Site.Helpers;

namespace TravelLine.Food.Site.Admin
{
    public partial class UserOrders : System.Web.UI.Page
    {
        private readonly ILegalService _legalService;
        private readonly IOrderService _orderService;
        private readonly IWorkTimeRepository _workTimeRepository;
        private IUserOrderReportBuilder _reportBuilder;

        public UserOrders( ILegalService legalService, IOrderService orderService, IWorkTimeRepository workTimeRepository )
        {
            _legalService = legalService;
            _orderService = orderService;
            _workTimeRepository = workTimeRepository;
        }

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                int year = DateTime.Today.Year;
                for ( int i = year; i >= year - 3; i-- )
                {
                    ddlYear.Items.Add( new ListItem() { Text = i.ToString(), Value = i.ToString() } );
                }

                ddlMonth.SelectedValue = DateTime.Today.Month.ToString();
                ddlYear.SelectedValue = DateTime.Today.Year.ToString();

                List<Legal> legals = _legalService.GetTLLegals();
                legals.Insert( 0, new Legal() { Id = 0, FullName = "Все" } );
                legals.Add( new Legal() { Id = ConfigService.StudentsID, FullName = "Студенты" } );

                ddlLegals.DataSource = legals;
                ddlLegals.DataBind();
            }

            Calculate();
        }

        private void Calculate()
        {
            int year = Int32.Parse( ddlYear.SelectedValue );
            int month = Int32.Parse( ddlMonth.SelectedValue );
            int legalId = Int32.Parse( ddlLegals.SelectedValue );

            var startDate = new DateTime( year, month, 1 );
            var endDate = new DateTime( year, month, DateTime.DaysInMonth( year, month ) );

            List<UserOrder> userOrders = _orderService.GetOrders( startDate, endDate );
            if( legalId != 0 )
            {
                userOrders = userOrders.FindAll( uo => uo.Legal.Id == legalId );
            }

            List<WorkTime> workTimes = _workTimeRepository.GetWorkTimes( startDate );

            switch( ( UserConsumptionsReportType )Int32.Parse( ddlType.SelectedValue ) )
            {
                case UserConsumptionsReportType.Daily:
                    _reportBuilder = new UserOrderDailyReportBuilder().Build( userOrders, workTimes );
                    break;
                case UserConsumptionsReportType.Transfer:
                    _reportBuilder = new UserOrderTransferReportBuilder().Build( userOrders, workTimes );
                    break;
                default:
                    _reportBuilder = new UserOrderComplexReportBuilder().Build( userOrders, workTimes );
                    break;
            }
        }

        protected string GetHTML()
        {
            return _reportBuilder.GetHtmlData();
        }

        protected void btnExport_Click( object sender, EventArgs e )
        {
            List<object[]> data = _reportBuilder.GetExcelData();

            using ( System.IO.MemoryStream stream = ExcelHelper.CreateExcel( data ) )
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader( "content-disposition", "attachment;filename=Report.xlsx" );
                Response.Charset = "";
                EnableViewState = false;
                stream.WriteTo( Response.OutputStream );
                Response.Flush();
                Response.End();
            }
        }
    }
}
