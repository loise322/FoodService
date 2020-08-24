using System;
using System.Collections;
using System.Web;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Domain.Dishes;

namespace TravelLine.Food.Core
{
    public class Library
    {
        public static string GetDateSelectionLink( DateTime date )
        {
            return String.Format( "~/Default.aspx?date={0}", date.ToShortDateString() );
        }


        public static String GetParamString( string paramName )
        {
            String paramValue = HttpContext.Current.Request[ paramName ];
            return paramValue == null ? "" : paramValue;
        }

        public static string GetStringType( DishType type )
        {
            var res = String.Empty;
            switch ( type )
            {
                case DishType.Garnish:
                    res = DishTypesStrings.Garnish;
                    break;
                case DishType.Salat:
                    res = DishTypesStrings.Salat;
                    break;
                case DishType.SecondDish:
                    res = DishTypesStrings.SecondDish;
                    break;
                case DishType.Soup:
                    res = DishTypesStrings.Soup;
                    break;
            }
            return res;
        }

        public static string Implode( string delim, int[] data )
        {
            string res = String.Empty;
            ArrayList strs = new ArrayList();

            for ( int i = 0; i < data.Length; i++ )
            {
                strs.Add( data[ i ].ToString() );
            }
            return String.Join( ",", strs.ToArray( typeof( string ) ) as string[] ); ;
        }

        public static string RusDayOfWeek( DayOfWeek day )
        {
            switch ( day )
            {
                case DayOfWeek.Friday:
                    return "Пятница";
                case DayOfWeek.Monday:
                    return "Понедельник";
                case DayOfWeek.Saturday:
                    return "Суббота";
                case DayOfWeek.Sunday:
                    return "Воскресенье";
                case DayOfWeek.Thursday:
                    return "Четверг";
                case DayOfWeek.Tuesday:
                    return "Вторник";
                case DayOfWeek.Wednesday:
                    return "Среда";
            }
            return "";
        }

        public static string RusMonth( int month )
        {
            string res = String.Empty;
            switch ( month )
            {
                case 1:
                    res = "Января";
                    break;
                case 2:
                    res = "Февраля";
                    break;
                case 3:
                    res = "Марта";
                    break;
                case 4:
                    res = "Апреля";
                    break;
                case 5:
                    res = "Мая";
                    break;
                case 6:
                    res = "Июня";
                    break;
                case 7:
                    res = "Июля";
                    break;
                case 8:
                    res = "Августа";
                    break;
                case 9:
                    res = "Сентября";
                    break;
                case 10:
                    res = "Октября";
                    break;
                case 11:
                    res = "Ноября";
                    break;
                case 12:
                    res = "Декабря";
                    break;

            }
            return res;
        }
    }
}
