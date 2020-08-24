using System.Collections.Generic;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Domain.WorkTimes;

namespace TravelLine.Food.Core.Reports
{
    public interface IUserOrderReportBuilder
    {
        IUserOrderReportBuilder Build( List<UserOrder> userOrders, List<WorkTime> workTimes );

        string GetHtmlData();

        List<object[]> GetExcelData();
    }
}
