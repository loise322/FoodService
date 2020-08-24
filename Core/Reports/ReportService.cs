using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Suppliers;

namespace TravelLine.Food.Core.Reports
{
    public class ReportService : IReportService
    {
        private readonly IOrderService _orderService;
        private readonly ISupplierService _supplierService;

        public ReportService( IOrderService orderService, ISupplierService supplierService )
        {
            _orderService = orderService;
            _supplierService = supplierService;
        }

        public DataTable OrderedList(
            DateTime date,
            int deliveryOffice,
            int supplierId,
            bool isStudents )
        {
            DataTable dt = new DataTable();
            dt.Columns.Add( "UserName" );
            dt.Columns.Add( "Salat" );
            dt.Columns.Add( "Soup" );
            dt.Columns.Add( "SecondDish" );
            dt.Columns.Add( "Garnish" );
            List<OrderModel> orders = _orderService.GetOrdersByDeliveryOffice( date, deliveryOffice, isStudents );
            foreach ( OrderModel order in orders )
            {
                foreach ( OrderItemModel item in order.Items )
                {
                    string salat = ( item.Salat != null && item.Salat.SupplierId == supplierId ) ? item.Salat.Name : "";
                    string soup = ( item.Soup != null && item.Soup.SupplierId == supplierId ) ? item.Soup.Name : "";
                    string secondDish = ( item.SecondDish != null && item.SecondDish.SupplierId == supplierId ) ? item.SecondDish.Name : "";
                    string garnish = ( item.Garnish != null && item.Garnish.SupplierId == supplierId ) ? item.Garnish.Name : "";

                    if ( !String.IsNullOrEmpty( salat )
                        || !String.IsNullOrEmpty( soup )
                        || !String.IsNullOrEmpty( secondDish )
                        || !String.IsNullOrEmpty( garnish ) )
                    {
                        dt.Rows.Add( order.User.Name, salat, soup, secondDish, garnish );
                    }
                }
            }

            return dt;
        }

        public DataTable GetRestaurantList(
            DateTime date,
            int deliveryOffice,
            int legal,
            int[] excludedLegals,
            int supplierId )
        {
            DataTable dt = new DataTable();
            dt.Columns.Add( "Name" );
            dt.Columns.Add( "Count" );
            dt.Columns.Add( "Cost" );
            dt.Columns.Add( "Sum" );

            List<OrderModel> orders = deliveryOffice > 0
                ? _orderService.GetOrdersByDeliveryOffice( date, deliveryOffice )
                : _orderService.GetOrdersByLegal( date, legal );

            SupplierModel supplier = _supplierService.GetSupplier( supplierId );

            if ( excludedLegals != null )
            {
                orders = orders.FindAll( o => !excludedLegals.Contains( o.User.GetUserLegal( date ).LegalId ) );
            }

            List<OrderItemModel> orderItems = orders.SelectMany( x => x.Items ).ToList();

            Hashtable salat = new Hashtable();
            Hashtable soup = new Hashtable();
            Hashtable second = new Hashtable();
            int secondDishWare = 0;
            int soupWare = 0;
            int salatWare = 0;

            foreach ( OrderItemModel item in orderItems )
            {
                DishModel salatDish = item.Salat != null && item.Salat.SupplierId == supplierId ? item.Salat : null;
                DishModel soupDish = item.Soup != null && item.Soup.SupplierId == supplierId ? item.Soup : null;
                DishModel garnish = item.Garnish != null && item.Garnish.SupplierId == supplierId ? item.Garnish : null;
                DishModel secondDish = item.SecondDish != null && item.SecondDish.SupplierId == supplierId ? item.SecondDish : null;

                if ( salatDish != null )
                {
                    AddObjects( ref salat, salatDish );
                }
                if ( soupDish != null )
                {
                    AddObjects( ref soup, soupDish );
                }

                if ( garnish != null || secondDish != null )
                {
                    AddObjects( ref second, secondDish, garnish );
                }

                secondDishWare += secondDish != null || garnish != null ? 1 : 0;
                soupWare += soupDish != null ? 1 : 0;
                salatWare += salatDish != null ? 1 : 0;
            }
            foreach ( object i in salat.Values )
            {
                ReportCount rep = ( ReportCount )i;
                dt.Rows.Add( rep.Element1.Name, rep.Count, rep.Element1.Cost.ToString( "N2" ), ( rep.Element1.Cost * rep.Count ).ToString( "N2" ) );
            }
            foreach ( object i in soup.Values )
            {
                ReportCount rep = ( ReportCount )i;
                dt.Rows.Add( rep.Element1.Name, rep.Count, rep.Element1.Cost.ToString( "N2" ), ( rep.Element1.Cost * rep.Count ).ToString( "N2" ) );
            }
            foreach ( object i in second.Values )
            {
                ReportCount rep = ( ReportCount )i;
                decimal cost = 0;
                StringBuilder name = new StringBuilder();
                if ( rep.Element1 != null )
                {
                    name.Append( rep.Element1.Name ); name.Append( "," );
                    cost += rep.Element1.Cost;
                }
                if ( rep.Element2 != null )
                {
                    name.Append( rep.Element2.Name ); name.Append( "," );
                    cost += rep.Element2.Cost;
                }
                if ( name.Length > 0 )
                {
                    name.Length--;
                }
                dt.Rows.Add( name.ToString(), rep.Count, cost.ToString( "N2" ), ( cost * rep.Count ).ToString( "N2" ) );
            }

            // other
            decimal secondWareCost = supplier.SecondWareCost;
            decimal soupWareCost = supplier.SoupWareCost;
            decimal salatWareCost = supplier.SalatWareCost;
            if ( secondDishWare > 0 )
            {
                dt.Rows.Add( "Одноразовая посуда (для второго)", secondDishWare, secondWareCost.ToString( "N2" ), ( secondWareCost * secondDishWare ).ToString( "N2" ) );
            }
            if ( soupWare > 0 )
            {
                dt.Rows.Add( "Одноразовая посуда (для супа)", soupWare, soupWareCost.ToString( "N2" ), ( soupWareCost * soupWare ).ToString( "N2" ) );
            }
            if ( salatWare > 0 )
            {
                dt.Rows.Add( "Одноразовая посуда (для салата)", salatWare, salatWareCost.ToString( "N2" ), ( salatWareCost * salatWare ).ToString( "N2" ) );
            }
            return dt;
        }

        private static void AddObjects( ref Hashtable list, DishModel dish )
        {

            if ( list[ dish.Id ] == null )
            {
                ReportCount res = new ReportCount
                {
                    Element1 = dish,
                    Count = 1
                };
                list[ dish.Id ] = res;
            }
            else
            {
                ReportCount res = ( ReportCount )list[ dish.Id ];
                res.Count++;
                list[ dish.Id ] = res;
            }
        }

        private static void AddObjects( ref Hashtable list, DishModel dish1, DishModel dish2 )
        {
            string key = ( ( dish1 != null ) ? dish1.Id.ToString() : "_" ) + ( ( dish2 != null ) ? dish2.Id.ToString() : "_" );
            if ( list[ key ] == null )
            {
                ReportCount res = new ReportCount
                {
                    Element1 = dish1,
                    Element2 = dish2,
                    Count = 1
                };
                list[ key ] = res;
            }
            else
            {
                ReportCount res = ( ReportCount )list[ key ];
                res.Count++;
                list[ key ] = res;
            }
        }
    }
}
