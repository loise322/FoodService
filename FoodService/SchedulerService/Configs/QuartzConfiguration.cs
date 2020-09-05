using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Configs
{
    public class QuartzConfiguration
    {
        public string PathDirectory { get; set; }

        public DailyTimeExecution TimeExecution { get; set; }
    }
}
