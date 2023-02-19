using Seagull.BarTender.Print;
using Seagull.BarTender.PrintServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace PDI_Feather_Tracking_WPF
{
    class BartenderPrintingHelper
    {


        public static string Print(string template_path, string printer_name)
        {
            using (Engine engine = new Engine())
            {
                engine.Start();

                LabelFormatDocument format = engine.Documents.Open(template_path);

                // string batch_no = format.Substrings["batch_no_component"].Value;

                format.SubStrings["batch_no_component"].Value = "C23094561";
                format.PrintSetup.NumberOfSerializedLabels = 1;
                format.PrintSetup.IdenticalCopiesOfLabel = 1;

                format.PrintSetup.PrinterName = printer_name;

                Result result = format.Print();
                format.Close(SaveOptions.DoNotSaveChanges);

                engine.Stop();
                return result.ToString();
            }
        }

        public static LabelFormatDocument ReadLabelDocument(string template_path)
        {
            using (Engine engine = new Engine())
            {
                engine.Start();
                LabelFormatDocument format = engine.Documents.Open(template_path);
                return format;
            }
        }
    }
}
