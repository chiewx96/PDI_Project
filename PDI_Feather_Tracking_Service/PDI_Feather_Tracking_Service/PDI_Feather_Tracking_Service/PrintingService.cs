using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using PDI_Feather_Tracking_Service.GeneralService;

namespace PDI_Feather_Tracking_Service
{
    public partial class PrintingService : ServiceBase
    {
        private const string filePath = "D:/Desktop/Log.txt";
        private IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        private TcpService TcpService;

        public PrintingService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TcpService tcpService = new TcpService(log_request);
          
        }

        protected override void OnStop()
        {
            TcpService.Dispose();
        }

        public void log_request(object sender)
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            try
            {
                File.OpenWrite(filePath);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Date:{DateTime.Now.Date}");
                sb.AppendLine($"Time:{DateTime.Now.TimeOfDay}");
                sb.AppendLine(sender.ToString());
                string[] content = { sb.ToString() };
                File.WriteAllLines(filePath, content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // TODO: Insert monitoring activities here.
        }
    }
}
