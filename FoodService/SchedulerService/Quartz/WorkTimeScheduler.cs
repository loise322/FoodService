using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SchedulerService.Configs;

namespace SchedulerService.Quartz
{
    public class WorkTimeScheduler
    {
        public static async void Start( IServiceProvider serviceProvider )
        {
            QuartzConfiguration quartzConfiguration = serviceProvider.GetService<QuartzConfiguration>();

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<FileChecker>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity( "DbTestTrigger", "default" )
                .StartNow()
                .WithDailyTimeIntervalSchedule(x => x
                  .WithIntervalInHours( 24 )
                  .OnEveryDay()
                  .StartingDailyAt( TimeOfDay.HourAndMinuteOfDay( quartzConfiguration.TimeExecution.Hours, quartzConfiguration.TimeExecution.Minutes ) ) )
                .Build();

            await scheduler.ScheduleJob( jobDetail, trigger );

        }

        public static async void Stop()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Shutdown();
        }
    }
}
