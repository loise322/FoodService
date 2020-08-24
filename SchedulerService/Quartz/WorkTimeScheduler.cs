using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

namespace SchedulerService.Quartz
{
    public class WorkTimeScheduler
    {
        public static async void Start( IServiceProvider serviceProvider )
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            scheduler.JobFactory = serviceProvider.GetService<JobFactory>();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<FileChecker>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity( "DbTestTrigger", "default" )
                .StartNow()
                .WithSimpleSchedule( x => x
                 .WithIntervalInSeconds( 40 )
                 .RepeatForever()  )
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
