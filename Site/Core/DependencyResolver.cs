using System.Configuration;
using System.Data.Entity;
using TravelLine.Food.Core.Calendar;
using TravelLine.Food.Core.DeliveryOffices;
using TravelLine.Food.Core.Dishes;
using TravelLine.Food.Core.DishQuotas;
using TravelLine.Food.Core.DishRatings;
using TravelLine.Food.Core.Legals;
using TravelLine.Food.Core.Menus;
using TravelLine.Food.Core.Orders;
using TravelLine.Food.Core.Reports;
using TravelLine.Food.Core.Suppliers;
using TravelLine.Food.Core.Users;
using TravelLine.Food.Domain.Configs;
using TravelLine.Food.Domain.DeliveryOffices;
using TravelLine.Food.Domain.Dishes;
using TravelLine.Food.Domain.DishQuotas;
using TravelLine.Food.Domain.DishRatings;
using TravelLine.Food.Domain.Legals;
using TravelLine.Food.Domain.Menus;
using TravelLine.Food.Domain.Orders;
using TravelLine.Food.Domain.Suppliers;
using TravelLine.Food.Domain.Users;
using TravelLine.Food.Domain.WorkTimes;
using TravelLine.Food.Infrastructure;
using TravelLine.Food.Infrastructure.Repositories;
using Unity;
using Unity.Injection;

namespace TravelLine.Food.Site.Core
{
    public static class DependencyResolver
    {
        private static IUnityContainer _container;


        public static void Load( IUnityContainer container )
        {
            container.RegisterType<DbContext, FoodContext>( new InjectionConstructor( ConfigurationManager.ConnectionStrings[ "DishDBConnectionString" ].ConnectionString ) );

            container.RegisterType<IConfigRepository, ConfigRepository>();
            container.RegisterType<IDishRepository, DishRepository>();
            container.RegisterType<IDishRatingRepository, DishRatingRepository>();
            container.RegisterType<IDishQuotaRepository, DishQuotaRepository>();
            container.RegisterType<IDeliveryOfficeRepository, DeliveryOfficeRepository>();
            container.RegisterType<IMenuRepository, MenuRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<ILegalRepository, LegalRepository>();
            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<IUserLegalRepository, UserLegalRepository>();
            container.RegisterType<IWorkTimeRepository, WorkTimeRepository>();

            container.RegisterType<ICalendarService, CalendarService>();
            container.RegisterType<IDishService, DishService>();
            container.RegisterType<IDishRatingService, DishRatingService>();
            container.RegisterType<IDishQuotaService, DishQuotaService>();
            container.RegisterType<IDeliveryOfficeService, DeliveryOfficeService>();
            container.RegisterType<IMenuService, MenuService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IReportService, ReportService>();
            container.RegisterType<ILegalService, LegalService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<ISupplierService, SupplierService>();

            _container = container;
        }

        public static T GetService<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
