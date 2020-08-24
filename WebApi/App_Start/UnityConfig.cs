using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishRatings;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Reports;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Configs;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.DishRatings;
using TravelLine.Food.Domain.DeliveryOffices;
using TravelLine.Food.Domain.Menus;
using TravelLine.Food.Domain.Orders;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Infrastructure;
using TravelLine.Food.Infrastructure.Repositories;
using Unity;
using Unity.Injection;
using Unity.WebApi;
using TravelLine.Food.Core.Import;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Core.Transfers;

namespace TravelLine.Food.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<DbContext, FoodContext>( new InjectionConstructor( ConfigurationManager.ConnectionStrings[ "DishDBConnectionString" ].ConnectionString ) );

            container.RegisterType<IConfigRepository, ConfigRepository>();
            container.RegisterType<IDishRatingRepository, DishRatingRepository>();
            container.RegisterType<IDishRepository, DishRepository>();
            container.RegisterType<IDeliveryOfficeRepository, DeliveryOfficeRepository>();
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<ILegalRepository, LegalRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserLegalRepository, UserLegalRepository>();
            container.RegisterType<IWorkTimeRepository, WorkTimeRepository>();

            container.RegisterType<ICalendarService, CalendarService>();
            container.RegisterType<IDishService, DishService>();
            container.RegisterType<IDishRatingService, DishRatingService>();
            container.RegisterType<IDeliveryOfficeService, DeliveryOfficeService>();
            container.RegisterType<IImportService, ImportService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<ILegalService, LegalService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ITransferService, TransferService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
