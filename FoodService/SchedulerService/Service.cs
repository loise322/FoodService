using System;
using System.Configuration;
using System.IO;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SchedulerService.Quartz;
using SchedulerService.Services.Import;
using TravelLine.Food.Infrastructure;

namespace SchedulerService
{
    public partial class Service : ServiceBase
    {
        private readonly WorkTimeScheduler scheduler;
        public Service()
        {
            InitializeComponent();
            CanStop = true;
            CanPauseAndContinue = true;
            AutoLog = true;
        }

        protected override void OnStart( string[] args )
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DishDBConnectionString"].ConnectionString;
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddScoped<FoodContext>( _ => new FoodContext( connectionString ) )
                .AddScoped<IImportService, ImportService>()
                .AddTransient<JobFactory>()
                .AddScoped<FileChecker>()
                .BuildServiceProvider();

            WorkTimeScheduler.Start(serviceProvider);
        }
        protected override void OnStop()
        {
            if ( scheduler != null )
            {
                WorkTimeScheduler.Stop();
            }
        }
    } 
}
