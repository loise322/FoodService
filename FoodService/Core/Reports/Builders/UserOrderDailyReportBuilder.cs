using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelLine.Food.Core.Configs;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Domain.WorkTimes;

namespace TravelLine.Food.Core.Reports
{
    public class UserOrderDailyReportBuilder : IUserOrderReportBuilder
    {
        private List<DateTime> _dates = new List<DateTime>();
        private Dictionary<int, string> _legals = new Dictionary<int, string>();
        private Dictionary<int, string> _users = new Dictionary<int, string>();
        private Dictionary<int, Dictionary<int, Dictionary<DateTime, decimal>>> _orders = new Dictionary<int, Dictionary<int, Dictionary<DateTime, decimal>>>();
        private Dictionary<int, Dictionary<DateTime, decimal>> _dailyOrdersSumm = new Dictionary<int, Dictionary<DateTime, decimal>>();
        private Dictionary<DateTime, decimal> _totalOrdersSumm = new Dictionary<DateTime, decimal>();

        public IUserOrderReportBuilder Build( List<UserOrder> userOrders, List<WorkTime> workTimes )
        {
            _orders = new Dictionary<int, Dictionary<int, Dictionary<DateTime, decimal>>>();

            foreach( UserOrder userOrder in userOrders.OrderBy( uo => uo.User.Name ) )
            {
                if( !_legals.ContainsKey( userOrder.Legal.Id ) )
                {
                    _legals.Add( userOrder.Legal.Id, userOrder.Legal.Name );
                    _orders.Add( userOrder.Legal.Id, new Dictionary<int, Dictionary<DateTime, decimal>>() );
                }

                if( !_users.ContainsKey( userOrder.User.Id ) )
                {
                    _users.Add( userOrder.User.Id, userOrder.User.Name );
                }

                if( !_orders[ userOrder.Legal.Id ].ContainsKey( userOrder.User.Id ) )
                {
                    _orders[ userOrder.Legal.Id ].Add( userOrder.User.Id, new Dictionary<DateTime, decimal>() );
                }

                if( !_dates.Contains( userOrder.Date ) )
                {
                    _dates.Add( userOrder.Date );
                }

                if( !_orders[ userOrder.Legal.Id ][ userOrder.User.Id ].ContainsKey( userOrder.Date ) )
                {
                    _orders[ userOrder.Legal.Id ][ userOrder.User.Id ].Add( userOrder.Date, 0 );
                }
                _orders[ userOrder.Legal.Id ][ userOrder.User.Id ][ userOrder.Date ] += userOrder.Cost + userOrder.DishesCost;

                // Считаем сумму заказа на день для команды
                if( !_dailyOrdersSumm.ContainsKey( userOrder.Legal.Id ) )
                {
                    _dailyOrdersSumm.Add( userOrder.Legal.Id, new Dictionary<DateTime, decimal>() );
                }
                if( !_dailyOrdersSumm[ userOrder.Legal.Id ].ContainsKey( userOrder.Date ) )
                {
                    _dailyOrdersSumm[ userOrder.Legal.Id ].Add( userOrder.Date, 0 );
                }
                _dailyOrdersSumm[ userOrder.Legal.Id ][ userOrder.Date ] += userOrder.Cost + userOrder.DishesCost;

                // Считаем общую сумму
                if( !_totalOrdersSumm.ContainsKey( userOrder.Date ) )
                {
                    _totalOrdersSumm.Add( userOrder.Date, 0 );
                }
                _totalOrdersSumm[ userOrder.Date ] += userOrder.Cost + userOrder.DishesCost;
            }
            _dates.Sort();

            return this;
        }

        public string GetHtmlData()
        {
            var result = new StringBuilder();
            result.Append( "<table class=\"userOrdersTable table table-bordered table-hover \" border=\"1\" cellpadding=\"0\" cellspacing=\"0\">\n" );

            foreach( var legal in _legals )
            {
                result.Append( "<tr>" );
                result.Append( "<th class='legalName' colspan='" + ( _dates.Count + 2 ) + "'>" + legal.Value + "</th>" );
                result.Append( "</tr>\n" );

                result.Append( "<tr>" );
                result.Append( "<td></td>" );
                foreach( DateTime date in _dates )
                {
                    result.Append( "<td><b>" + date.ToString( "MM/dd" ) + "</b></td>" );
                }
                result.Append( "<td><b>Сумма</b></td>" );
                result.Append( "</tr>\n" );

                foreach( KeyValuePair<int, Dictionary<DateTime, decimal>> userOrderRow in _orders[ legal.Key ] )
                {
                    result.Append( "<tr>" );
                    result.Append( "<td class='userName'>" + _users[ userOrderRow.Key ] + "</td>" );

                    var userDatesOrders = userOrderRow.Value;
                    decimal summ = 0;
                    foreach( var date in _dates )
                    {
                        decimal value = 0;
                        if( userDatesOrders.ContainsKey( date ) )
                        {
                            value = userDatesOrders[ date ];
                            result.AppendFormat( "<td>{0:N2}</td>", value );
                            summ += value;
                        }
                        else
                        {
                            result.Append( "<td>&nbsp;</td>" );
                        }           
                    }

                    result.AppendFormat( "<td>{0:N2}</td>", summ );
                    result.Append( "</tr>" );
                }

                result.Append( "<tr>" );
                result.Append( "<td class='userName'><b>Сумма по дням</b></td>" );

                foreach( var date in _dates )
                {
                    decimal value = 0;
                    if( _dailyOrdersSumm[ legal.Key ].ContainsKey( date ) )
                    {
                        value = _dailyOrdersSumm[ legal.Key ][ date ];
                        result.AppendFormat( "<td>{0:N2}</td>", value );
                    }
                    else
                    {
                        result.Append( "<td>&nbsp;</td>" );
                    }
                }

                result.AppendFormat( "<td>{0:N2}</td>", _dailyOrdersSumm[ legal.Key ].Values.Sum() );
                result.Append( "</tr>" );
            }

            result.Append( "<tr>" );
            result.Append( "<td class='userName'><b>Итого</b></td>" );

            foreach( var date in _dates )
            {
                decimal value = 0;
                if( _totalOrdersSumm.ContainsKey( date ) )
                {
                    value = _totalOrdersSumm[ date ];
                    result.AppendFormat( "<td>{0:N2}</td>", value );
                }
                else
                {
                    result.Append( "<td>&nbsp;</td>" );
                }
            }

            result.AppendFormat( "<td>{0:N2}</td>", _totalOrdersSumm.Values.Sum() );
            result.Append( "</tr>" );

            result.Append( "</table>\n" );
            result.Append( "<br />\n" );


            return result.ToString();
        }

        public List<object[]> GetExcelData()
        {
            var data = new List<object[]>();
            foreach( var legal in _legals )
            {
                data.Add( new string[] { legal.Value } );

                var headers = new List<string>();
                headers.Add( String.Empty );
                foreach( var date in _dates )
                {
                    headers.Add( date.ToString( "MM/dd" ) );
                }
                headers.Add( "Сумма" );
                data.Add( headers.ToArray() );

                foreach( KeyValuePair<int, Dictionary<DateTime, decimal>> userOrderRow in _orders[ legal.Key ] )
                {
                    var row = new List<object>();

                    row.Add( _users[ userOrderRow.Key ] );

                    var userDatesOrders = userOrderRow.Value;
                    decimal summ = 0;
                    foreach( var date in _dates )
                    {
                        decimal value = 0;
                        if( userDatesOrders.ContainsKey( date ) )
                        {
                            value = userDatesOrders[ date ];
                        }
                        row.Add( value );
                        summ += value;
                    }

                    row.Add( summ );

                    data.Add( row.ToArray() );
                }

                var totalRow = new List<object>();
                totalRow.Add( "Сумма по дням" );
                foreach( var date in _dates )
                {
                    decimal value = 0;
                    if( _dailyOrdersSumm[ legal.Key ].ContainsKey( date ) )
                    {
                        value = _dailyOrdersSumm[ legal.Key ][ date ];
                    }
                    totalRow.Add( value );
                }
                totalRow.Add( _dailyOrdersSumm[ legal.Key ].Values.Sum() );
                data.Add( totalRow.ToArray() );
            }

            var summRow = new List<object>();
            summRow.Add( "Итого" );
            foreach( var date in _dates )
            {
                decimal value = 0;
                if( _totalOrdersSumm.ContainsKey( date ) )
                {
                    value = _totalOrdersSumm[ date ];
                }
                summRow.Add( value );
            }
            summRow.Add( _totalOrdersSumm.Values.Sum() );
            data.Add( summRow.ToArray() );

            return data;
        }
    }
}
