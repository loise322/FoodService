using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Domain.WorkTimes;

namespace TravelLine.Food.Core.Reports
{
    public class UserOrderComplexReportBuilder : IUserOrderReportBuilder
    {
        private List<ComplexReportItem> _reportItems;

        public IUserOrderReportBuilder Build( List<UserOrder> userOrders, List<WorkTime> workTimes )
        {
            _reportItems = new List<ComplexReportItem>();
            if( userOrders.Count == 0 )
            {
                return this;
            }

            var month = new DateTime( userOrders[ 0 ].Date.Year, userOrders[ 0 ].Date.Month, 1 );
            int priceQuota = ConfigService.GetUserDayPriceQuota( month );
            var dates = new List<DateTime>();
            var legals = new Dictionary<int, string>();
            var users = new Dictionary<int, string>();
            var orders = new Dictionary<int, Dictionary<int, Dictionary<DateTime, decimal>>>();

            foreach ( UserOrder userOrder in userOrders.OrderBy( uo => uo.User.Name ) )
            {
                if ( !legals.ContainsKey( userOrder.Legal.Id ) )
                {
                    legals.Add( userOrder.Legal.Id, userOrder.Legal.Name );
                    orders.Add( userOrder.Legal.Id, new Dictionary<int, Dictionary<DateTime, decimal>>() );
                }

                if ( !users.ContainsKey( userOrder.User.Id ) )
                {
                    users.Add( userOrder.User.Id, userOrder.User.Name );
                }

                if ( !orders[ userOrder.Legal.Id ].ContainsKey( userOrder.User.Id ) )
                {
                    orders[ userOrder.Legal.Id ].Add( userOrder.User.Id, new Dictionary<DateTime, decimal>() );
                }

                if ( !dates.Contains( userOrder.Date ) )
                {
                    dates.Add( userOrder.Date );
                }

                if ( !orders[ userOrder.Legal.Id ][ userOrder.User.Id ].ContainsKey( userOrder.Date ) )
                {
                    orders[ userOrder.Legal.Id ][ userOrder.User.Id ].Add( userOrder.Date, 0 );
                }
                orders[ userOrder.Legal.Id ][ userOrder.User.Id ][ userOrder.Date ] += userOrder.Cost + userOrder.DishesCost;
            }

            foreach ( KeyValuePair<int, string> legal in legals )
            {
                foreach ( KeyValuePair<int, Dictionary<DateTime, decimal>> userOrderRow in orders[ legal.Key ] )
                {
                    Dictionary<DateTime, decimal> userDatesOrders = userOrderRow.Value;
                    decimal summ = 0;

                    int days = 0;
                    int workdays = workTimes.FirstOrDefault( wt => wt.UserId == userOrderRow.Key && wt.Month == month && wt.LegalId == legal.Key )?.Days ?? 0;

                    foreach ( DateTime date in dates )
                    {
                        decimal value = 0;
                        if ( userDatesOrders.ContainsKey( date ) )
                        {
                            value = userDatesOrders[ date ];
                            days++;
                        }
                        summ += value;
                    }

                    _reportItems.Add( new ComplexReportItem()
                    {
                        LegalName = legal.Value,
                        UserName = users[ userOrderRow.Key ],
                        Count = days,
                        WorkDays = workdays,
                        Sum = summ,
                        SumQuota = Math.Min( workdays, days ) * priceQuota,
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
            result.Append( "  <th>Организация</th>" );
            result.Append( "  <th>ФИО</th>" );
            result.Append( "  <th>Фактически отработанные дни (явки по табелю)</th>" );
            result.Append( "  <th>Заказно обедов по факту</th>" );
            result.Append( "  <th>Разница между заказами обедов и фактически отработанными днями</th>" );
            result.Append( "  <th>Сумма по нормативу</th>" );
            result.Append( "  <th>Сумма съеденного по факту</th>" );
            result.Append( "  <th>Контроль (сотрудник должен нам)</th>" );
            result.Append( "</tr>" );

            // Данные
            foreach ( ComplexReportItem item in _reportItems )
            {
                result.AppendLine( "<tr>" );
                result.AppendFormat( "<td class='userName'>{0}</td>", item.LegalName );
                result.AppendFormat( "<td class='userName'>{0}</td>", item.UserName );
                result.AppendFormat( "<td>{0}</td>", item.WorkDays );
                result.AppendFormat( "<td>{0}</td>", item.Count );
                result.AppendFormat( "<td>{0}</td>", item.Count - item.WorkDays );
                result.AppendFormat( "<td>{0:N2}</td>", item.SumQuota );
                result.AppendFormat( "<td>{0:N2}</td>", item.Sum );
                result.AppendFormat( "<td>{0:N2}</td>", item.Balance );
                result.AppendLine( "</tr>" );
            }

            // Итого
            result.AppendLine( "<tr>" );
            result.AppendLine( "<td></td>" );
            result.AppendLine( "<td class='userName'><b>Итого</b></td>" );
            result.AppendLine( "<td></td>" );
            result.AppendLine( "<td></td>" );
            result.AppendLine( "<td></td>" );
            result.AppendLine( "<td></td>" );
            result.AppendFormat( "<td>{0:N2}</td>", _reportItems.Sum( i => i.Sum ) );
            result.AppendLine( "<td></td>" );
            result.AppendLine( "</tr>" );

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
                    "Организация",
                    "ФИО",
                    "Фактически отработанные дни (явки по табелю)",
                    "Заказно обедов по факту",
                    "Разница между заказами обедов и фактически отработанными днями",
                    "Сумма по нормативу",
                    "Сумма съеденного по факту",
                    "Контроль (сотрудник должен нам)" }
            };

            // Данные
            foreach ( ComplexReportItem item in _reportItems )
            {
                data.Add( new object[] { item.LegalName, item.UserName, item.WorkDays, item.Count, item.Count - item.WorkDays, item.SumQuota, item.Sum, item.Balance } );
            }

            // Итого
            data.Add( new object[] { "", "Итого", "", "", "", "", _reportItems.Sum( i => i.Sum ), "" } );

            return data;
        }
    }
}
