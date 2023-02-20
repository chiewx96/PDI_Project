using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.Helper
{
    public class ReportHelper
    {

        public static void GenerateIncomingReport(List<InventoryRecords> records, string filepath)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(nameof(InventoryRecords.BatchNo));
            dt.Columns.Add(nameof(InventoryRecords.GrossWeight));
            dt.Columns.Add(nameof(InventoryRecords.TareWeight));
            dt.Columns.Add(nameof(InventoryRecords.NettWeight));
            dt.Columns.Add(nameof(InventoryRecords.IncomingDateTime));
            dt.Columns.Add(nameof(InventoryRecords.SkuType.Code));

            records.ForEach(x => dt.Rows.Add(new object[]
            { x.BatchNo, x.GrossWeight, x.TareWeight, x.NettWeight, x.IncomingDateTime, x.SkuType.Code }));

            byte[] filecontent = PDFHelper.GeneratePdf(dt);
            string filename = "Incoming_Report_PDF_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
            File.WriteAllBytes(Path.Combine(filepath, filename), filecontent);
        }
    }
}
