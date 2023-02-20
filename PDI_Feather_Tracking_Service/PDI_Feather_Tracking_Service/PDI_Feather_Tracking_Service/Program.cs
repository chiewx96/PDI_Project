using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        private static IConfiguration Configuration { get; set; }
        
        static void Main()
        {
            //string folderpath = "C:\\Users\\GMT-NB11\\Project\\PDI\\PDI-Feather-Tracking\\PDI_Feather_Tracking_WPF\\PDI_Feather_Tracking_WPF\\bin\\Debug\\net6.0-windows";
            string folderpath = "D:\\Projects\\PDI_Feather_Tracking\\PDI_Feather_Tracking_WPF\\PDI_Feather_Tracking_WPF\\bin\\Debug\\net6.0-windows";
            var builder = new ConfigurationBuilder().SetBasePath(folderpath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            Global.SetGlobalProperty(Configuration);

#if (!DEBUG)

     ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new PrintingService()
            };
            ServiceBase.Run(ServicesToRun);

#else

            PrintingService myServ = new PrintingService();
            myServ.Start();
#endif
          
        }
    }
}
