using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.Helper
{
    public static class ServiceHelper
    {
        public static bool RunService(string serviceName)
        {
            ServiceController sc = new ServiceController();
            sc.ServiceName = serviceName;

            if (sc.Status == ServiceControllerStatus.Running ||
                sc.Status == ServiceControllerStatus.StartPending)
            {
                Debug.WriteLine("Service is already running");
            }
            else
            {
                try
                {
                    Debug.Write("Start pending... ");
                    sc.Start();
                    sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));

                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        Debug.WriteLine("Service started successfully.");
                        return true;
                    }
                    else
                    {
                        Debug.WriteLine("Service not started.");
                        Debug.WriteLine("Current State: {0}", sc.Status.ToString("f"));
                    }
                }
                catch (InvalidOperationException)
                {
                    Debug.WriteLine("Could not start the service.");
                }
            }
            return false;
        }
    }
}
