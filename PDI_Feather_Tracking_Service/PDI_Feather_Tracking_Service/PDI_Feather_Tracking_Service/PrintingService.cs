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
using Seagull.BarTender.Print;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PDI_Feather_Tracking_Service
{
    public partial class PrintingService : ServiceBase
    {
        private TcpService TcpService;

        public PrintingService()
        {
            InitializeComponent();
        }

        public void Start()
        {
            //PrintLabel("C239876Sample");
            TcpService tcpService = new TcpService(PrintLabel, log_request, Global.PrintServicePort);

        }

        protected override void OnStart(string[] args)
        {
            TcpService tcpService = new TcpService(PrintLabel, log_request, Global.PrintServicePort);
        }

        protected override void OnStop()
        {
            TcpService.Dispose();
        }

        public void log_request(object sender)
        {
            try
            {
                if (!System.IO.File.Exists(Global.LogFilePath))
                    System.IO.File.Create(Global.LogFilePath);
                StringBuilder sb = new StringBuilder();
                //sb.AppendLine($"Date:{DateTime.Now.Date}");
                //sb.AppendLine($"Time:{DateTime.Now.TimeOfDay}");
                sb.AppendLine($"Time:{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} Event : {sender.ToString()}");
                using (StreamWriter sw = System.IO.File.AppendText(Global.LogFilePath))
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

        public void readLabel(object? sender)
        {
            LabelFormatDocument item = BartenderService.ReadLabelDocument(Global.LabelTemplatePath);
            log_request(item);
        }

        public void PrintLabel(object sender)
        {
            try
            {
                if (sender is string json_string)
                {
                    var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_string);
                    string result = BartenderService.Print(json["batch_no"], json["gross_weight"], json_string,
                        Global.LabelTemplatePath, Global.PrinterName);
                    log_request(result);
                }
            }
            catch (Exception e)
            {
                log_request(e.Message);
            }
        }
    }
}
