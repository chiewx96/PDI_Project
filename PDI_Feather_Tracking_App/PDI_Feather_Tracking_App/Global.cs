using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_App
{
    internal static class Global
    {
        internal static string PrinterName { get; private set; }
        internal static string LogFilePath { get; private set; }
        internal static string LabelTemplatePath { get; private set; }
        internal static int LogServicePort { get; private set; }
        internal static int PrintServicePort { get; private set; }

        internal static void SetGlobalProperty(IConfiguration configuration)
        {
            LogServicePort = int.Parse(configuration.GetSection("LogServicePort").Value);
            PrintServicePort = int.Parse(configuration.GetSection("PrintServicePort").Value);
            PrinterName = configuration.GetSection("PrinterName").Value.ToString();
            LogFilePath = configuration.GetSection("LogFilePath").Value.ToString();
            LabelTemplatePath = configuration.GetSection("LabelTemplatePath").Value.ToString();
        }
    }
}