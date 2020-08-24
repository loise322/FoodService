using System;
using Quartz;
using Quartz.Spi;

namespace SchedulerService.Quartz
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _provider;

        public JobFactory( IServiceProvider provider )
        {
            _provider = provider;
        }

        public IJob NewJob( TriggerFiredBundle bundle, IScheduler scheduler )
        {
            return new JobWrapper( _provider, bundle.JobDetail.JobType );
        }

        public void ReturnJob( IJob job )
        {
            ( job as IDisposable )?.Dispose();
        }
    }
}
