using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PDI_Feather_Tracking_App.Service;
using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_App
{
    internal class Program
    {
        private static IConfiguration Configuration { get; set; }

        static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            string folderpath = "D:\\Projects\\PDI_Feather_Tracking\\PDI_Feather_Tracking_WPF\\PDI_Feather_Tracking_WPF\\bin\\Debug\\net6.0-windows";
            var builder = new ConfigurationBuilder().SetBasePath(folderpath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            Global.SetGlobalProperty(Configuration);
            Start();
        }

        static void Start()
        {
            Task log_task = Task.Run(initializeLogService, cancellationTokenSource.Token);
            Task printer_task = Task.Run(initializePrinterService, cancellationTokenSource.Token);
            Task.WaitAll(log_task, printer_task);
        }

        static void initializeLogService()
        {
            TcpService logService = new TcpService(log_request, log_request, Global.LogServicePort);
        }

        static void initializePrinterService()
        {
            var printerService = new TcpService(PrintLabel, log_request, Global.PrintServicePort);
        }

        static string log_request(object sender)
        {
            try
            {
                if (!Directory.Exists(Global.LogFilePath))
                    Directory.CreateDirectory(Global.LogFilePath);
                if (!System.IO.File.Exists(Path.Combine(Global.LogFilePath, DateTime.Now.ToString("yyyyMMdd"))))
                    System.IO.File.Create(Path.Combine(Global.LogFilePath, DateTime.Now.ToString("yyyyMMdd")));
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Time:{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} Event : {sender.ToString()}");
                //using (StreamWriter sw = System.IO.File.AppendText(Path.Combine(Global.LogFilePath, DateTime.Now.ToString("yyyyMMdd"))))
                //{
                //    sw.WriteLine(sb.ToString());
                //}
                Console.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Console.WriteLine(ex.Message);
            }
            return string.Empty;
        }

        static void readLabel(object? sender)
        {
            LabelFormatDocument item = BartenderService.ReadLabelDocument(Global.LabelTemplatePath);
            log_request(item);
        }

        static string PrintLabel(object sender)
        {
            try
            {
                if (sender is string json_string)
                {
                    var json = JsonConvert.DeserializeObject<Dictionary<string, string>>(json_string);
                    log_request($"Start printing : {json["batch_no"]}");
                    string result = BartenderService.Print(json["batch_no"], json["gross_weight"], json["batch_no"],
                        Global.LabelTemplatePath, Global.PrinterName);
                    log_request($"End printing, Status : {result}");
                    return result;
                }
                return "invalid";
            }
            catch (Exception e)
            {
                log_request(e.Message);
                return e.Message;
            }
        }
    }
}
