using System;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SchedulerService.Configs;
using SchedulerService.Quartz;
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

            ServiceProvider serviceProvider = new ServiceCollection()
                .AddTransient<JobFactory>()
                .AddScoped<FileChecker>()
                .AddScoped<HttpClient>( _ => new HttpClient { BaseAddress = new Uri( "http://localhost:32213/" ) } )
                .AddScoped<QuartzConfiguration>( _ => new QuartzConfiguration
                {
                    PathDirectory = @"E:\checkScheduler",
                    ApiUrl = "api/import/from1c",
                    TimeExecution = new DailyTimeExecution 
                    {
                        Hours = 8,
                        Minutes = 0
                    }
                } )
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
