using Seagull.BarTender.Print;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_App.Service
{
    internal class BartenderService
    {
        public static string Print(string batch_no, string gross_weight, string title, string qr_code_data, string template_path, string printer_name)
        {
            using (Engine engine = new Engine())
            {
                engine.Start();

                LabelFormatDocument format = engine.Documents.Open(template_path);

                format.SubStrings["title"].Value = title;
                format.SubStrings["gross_weight"].Value = gross_weight;
                format.SubStrings["batch_no"].Value = batch_no;
                format.SubStrings["qr_code"].Value = qr_code_data;
                format.PrintSetup.NumberOfSerializedLabels = 1;

                format.PrintSetup.PrinterName = printer_name;
                format.PageSetup.Units = Units.Millimeters;

                Result result = format.Print();
                format.Close(SaveOptions.DoNotSaveChanges);

                engine.Stop();
                return result.ToString();
            }
        }

        public static LabelFormatDocument ReadLabelDocument(string template_path)
        {
            using (Engine engine = new Engine(true))
            {
                engine.Start();
                LabelFormatDocument format = engine.Documents.Open(template_path);
                return format;
            }
        }
    }
}
