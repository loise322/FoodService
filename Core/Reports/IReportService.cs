using System;
using System.Data;

namespace TravelLine.Food.Core.Reports
{
    public interface IReportService
    {
        /// <summary>
        /// Заказ в столовую
        /// </summary>
        /// <param name="date"></param>
        /// <param name="deliveryOffice"></param>
        /// <param name="legal"></param>
        /// <param name="excludedLegals"></param>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        DataTable GetRestaurantList( 
            DateTime date, 
            int deliveryOffice, 
            int legal, 
            int[] excludedLegals,
            int supplierId );

        /// <summary>
        /// Используется для показа заказанных блюд сотрудникам
        /// </summary>
        /// <param name="date"></param>
        /// <param name="deliveryOffice"></param>
        /// <param name="supplierId"></param>
        /// <param name="isStudents"></param>
        /// <returns></returns>
        DataTable OrderedList( DateTime date, int deliveryOffice, int supplierId, bool isStudents );
    }
}
