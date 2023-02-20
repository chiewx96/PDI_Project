using Seagull.BarTender.Print;

namespace PDI_Feather_Tracking_Service
{
    internal class BartenderService
    {
        public static string Print(string batch_no, string gross_weight, string template_path, string printer_name)
        {
            using (Engine engine = new Engine())
            {
                engine.Start();

                LabelFormatDocument format = engine.Documents.Open(template_path);

                // string batch_no = format.Substrings["batch_no_component"].Value;

                format.SubStrings["gross_weight"].Value = gross_weight;
                format.SubStrings["batch_no"].Value = batch_no;
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
            using (Engine engine = new Engine(true))
            {
                engine.Start();
                LabelFormatDocument format = engine.Documents.Open(template_path);
                return format;
            }
        }
    }
}
