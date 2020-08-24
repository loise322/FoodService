using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace SchedulerService
{
    [RunInstaller( true )]
    public partial class Installer : System.Configuration.Install.Installer
    {
        private readonly ServiceInstaller serviceInstaller;
        private readonly ServiceProcessInstaller processInstaller;

        public Installer()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller() 
            {
                StartType = ServiceStartMode.Manual,
                ServiceName = "SchedulerService"
            };
            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };
            Installers.Add( processInstaller );
            Installers.Add( serviceInstaller );
        }
    }
}
