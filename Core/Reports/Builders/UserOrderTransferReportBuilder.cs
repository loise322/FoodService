using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Domain.WorkTimes;

namespace TravelLine.Food.Core.Reports
{
    public class UserOrderTransferReportBuilder : IUserOrderReportBuilder
    {
        private readonly List<TransferReportItem> _reportItems = new List<TransferReportItem>();

        public IUserOrderReportBuilder Build( List<UserOrder> userOrders, List<WorkTime> workTimes )
        {
            var month = new DateTime( userOrders[ 0 ].Date.Year, userOrders[ 0 ].Date.Month, 1 );
            int priceQuota = ConfigService.GetUserDayPriceQuota( month );
            var dates = new List<DateTime>();
            var legals = new Dictionary<int, string>();
            var users = new Dictionary<int, string>();
            var orders = new Dictionary<int, Dictionary<int, Dictionary<DateTime, decimal>>>();

            foreach ( IGrouping<string, UserOrder> group in userOrders.OrderBy( uo => uo.User.Name ).GroupBy( uo => uo.User.Name ) )
            {
                var transerOrders = group.Where( g => g.Legal.Id == ConfigService.StudentsID ).ToList();
                var basicOrders = group.Where( g => g.Legal.Id != ConfigService.StudentsID ).ToList();

                if ( transerOrders.Count > 0 && group.First().User.UserLegals[ 0 ].LegalId != ConfigService.StudentsID )
                {
                    _reportItems.Add( new TransferReportItem()
                    {
                        UserName = group.Key,
                        SumTransfer = transerOrders.Sum( to => to.Cost ),
                        CountTransfer = transerOrders.Count,
                        BalanceTransfer = ( transerOrders.Count * priceQuota ) - transerOrders.Sum( to => to.Cost + to.DishesCost ),
                        SumBasic = basicOrders.Sum( to => to.Cost ),
                        CountBasic = basicOrders.Count,
                        BalanceBasic = ( basicOrders.Count * priceQuota ) - basicOrders.Sum( to => to.Cost + to.DishesCost )
                    } );
                }
            }

            return this;
        }

        public string GetHtmlData()
        {
            var result = new StringBuilder();

            result.AppendLine( "<table class=\"userOrdersTable table table-bordered table-hover \" border=\"1\" cellpadding=\"0\" cellspacing=\"0\">" );

            // Заголовки
            result.Append( "<tr>" );
            result.Append( "  <th>ФИО</th>" );
            result.Append( "  <th>Сумма съеденного по факту</th>" );
            result.Append( "  <th>Заказно обедов по факту</th>" );
            result.Append( "  <th>Студенты</th>" );
            result.Append( "  <th>Сумма съеденного по факту</th>" );
            result.Append( "  <th>Заказно обедов по факту</th>" );
            result.Append( "  <th>Юр. лицо</th>" );
            result.Append( "  <th>Общее кол-во заказов</th>" );
            result.Append( "  <th>Контроль</th>" );
            result.Append( "</tr>" );

            // Данные
            foreach ( TransferReportItem item in _reportItems )
            {
                result.AppendLine( "<tr>" );
                result.AppendFormat( "<td class='userName'>{0}</td>", item.UserName );
                result.AppendFormat( "<td>{0:N2}</td>", item.SumTransfer );
                result.AppendFormat( "<td>{0}</td>", item.CountTransfer );
                result.AppendFormat( "<td>{0:N2}</td>", item.BalanceTransfer );
                result.AppendFormat( "<td>{0:N2}</td>", item.SumBasic );
                result.AppendFormat( "<td>{0}</td>", item.CountBasic );
                result.AppendFormat( "<td>{0:N2}</td>", item.BalanceBasic );
                result.AppendFormat( "<td>{0}</td>", item.CountTotal );
                result.AppendFormat( "<td>{0:N2}</td>", item.BalanceTotal );
                result.AppendLine( "</tr>" );
            }

            result.AppendLine( "</table>" );
            result.AppendLine( "<br />" );

            return result.ToString();
        }

        public List<object[]> GetExcelData()
        {
            var data = new List<object[]>
            {
                // Заголовки
                new string[] {
                    "ФИО",
                    "Сумма съеденного по факту",
                    "Заказно обедов по факту",
                    "Студенты",
                    "Сумма съеденного по факту",
                    "Заказно обедов по факту",
                    "Юр. лицо",
                    "Общее кол-во заказов",
                    "Контроль" }
            };

            // Данные
            foreach ( TransferReportItem item in _reportItems )
            {
                data.Add( new object[] { item.UserName, item.SumTransfer, item.CountTransfer, item.BalanceTransfer, item.SumBasic, item.CountBasic, item.BalanceBasic, item.CountTotal, item.BalanceTotal } );
            }

            return data;
        }
    }
}
