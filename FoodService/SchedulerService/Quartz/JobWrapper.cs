using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace SchedulerService.Quartz
{
    public class JobWrapper : IJob, IDisposable
    {
        private readonly IServiceScope _serviceScope;
        private readonly IJob _job;

        public JobWrapper( IServiceProvider serviceProvider, Type jobType )
        {
            _serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _job = ActivatorUtilities.CreateInstance( _serviceScope.ServiceProvider, jobType ) as IJob;
        }

        public Task Execute( IJobExecutionContext context )
        {
            return _job.Execute( context );
        }

        public void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}
