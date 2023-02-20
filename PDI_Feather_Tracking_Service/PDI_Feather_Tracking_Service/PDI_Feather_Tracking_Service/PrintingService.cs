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
using static System.Net.WebRequestMethods;

namespace PDI_Feather_Tracking_Service
{
    public partial class PrintingService : ServiceBase
    {
        //private const string filePath = "D:/Desktop/Log.txt";
        private const string filePath = "C:\\Users\\GMT-NB11\\Documents\\Debug\\Log.txt";


        private IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        private TcpService TcpService;

        public PrintingService()
        {
            InitializeComponent();
        }

        public void Start()
        {
            TcpService tcpService = new TcpService(log_request);

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
            try
            {
                if (!System.IO.File.Exists(filePath))
                    System.IO.File.Create(filePath);
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine($"Date:{DateTime.Now.Date}");
                //sb.AppendLine($"Time:{DateTime.Now.TimeOfDay}");
                sb.AppendLine($"Time:{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}");
                sb.AppendLine(sender.ToString());
                using (StreamWriter sw = System.IO.File.AppendText(filePath))
                {
                    sw.WriteLine(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // TODO: Insert monitoring activities here.
        }
    }
}
