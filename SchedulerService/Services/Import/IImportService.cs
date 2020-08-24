using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulerService.Services.Import
{
    public interface IImportService
    {
        void ImportFrom1c( Stream input );
    }
}
