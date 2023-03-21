using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public static void GenerateIncomingReport(List<InventoryRecords> records, string folderPath)
        {
            Directory.CreateDirectory(folderPath);

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn
            {
                Caption = "Batch No",
                ColumnName = nameof(InventoryRecords.BatchNo)
            });
            dt.Columns.Add(new DataColumn
            {
                Caption = "Gross Weight",
                ColumnName = nameof(InventoryRecords.GrossWeight)
            });
            dt.Columns.Add(new DataColumn
            {
                Caption = "Tare Weight",
                ColumnName = nameof(InventoryRecords.TareWeight)
            });
            dt.Columns.Add(new DataColumn
            {
                Caption = "Nett Weight",
                ColumnName = nameof(InventoryRecords.NettWeight)
            });
            dt.Columns.Add(new DataColumn
            {
                Caption = "Incoming",
                ColumnName = nameof(InventoryRecords.IncomingDateTime)
            });
            dt.Columns.Add(new DataColumn
            {
                Caption = "Sku",
                ColumnName = nameof(InventoryRecords.SkuType.Code)
            });

            records.ForEach(x => dt.Rows.Add(new object[]
            { x.BatchNo, x.GrossWeight, x.TareWeight, x.NettWeight, x.IncomingDateTime, x.SkuType.Code }));

            byte[] filecontent = PDFHelper.GeneratePdf(dt);
            string filename = "Incoming_Report_PDF_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
            string report_full_path = Path.Combine(folderPath, filename);
            File.WriteAllBytes(report_full_path, filecontent);
            General.SendNotifcation($"Report Path :{report_full_path}");
        }

        internal static void GenerateActualWeightList(List<InventoryRecords> filteredInventories, string path)
        {
            DataTable dt = new DataTable();
            GenerateActualWeightListDT(filteredInventories, ref dt);
            byte[] filecontent = PDFHelper.GeneratePdf(dt);
            string filename = "Actual_Weight_PDF_" + DateTime.Now.ToString("MMddyyyyhhmmss") + ".pdf";
            File.WriteAllBytes(Path.Combine(path, filename), filecontent);
        }

        private static void GenerateActualWeightListDT(List<InventoryRecords> filteredInventories, ref DataTable dt)
        {
            int count_repeatance = filteredInventories.Count / 25;

            //generate header
            for (var i = 0; i < count_repeatance + 1; i++)
            {
                dt.Columns.Add(new DataColumn
                {
                    Caption = "No",
                    ColumnName = $"No_{i}"
                });

                dt.Columns.Add(new DataColumn
                {
                    Caption = "Nett",
                    ColumnName = $"Nett_{i}"
                });
                dt.Columns.Add(new DataColumn
                {
                    Caption = "Gross",
                    ColumnName = $"Gross_{i}"
                });


            }


            Object[] summaryObj = new object[(count_repeatance + 1) * 3];
            for (var z = 0; z < summaryObj.Length; z++)
            {
                summaryObj[z] = new Decimal(0);
            }

            for (int i = 0; i < 27; i++)
            {
                Object[] obj = new object[(count_repeatance + 1) * 3];
                for (var j = 0; j < (count_repeatance + 1); j++)
                {
                    if (i == 0)
                    {
                        obj[j * 3] = string.Empty;
                        obj[(j * 3) + 1] = "KG";
                        obj[(j * 3) + 2] = "KG";

                    }
                    else if (i == 26)
                    {
                        obj[j * 3] = string.Empty;
                        obj[(j * 3) + 1] = summaryObj[(j * 3) + 1];
                        obj[(j * 3) + 2] = summaryObj[(j * 3) + 2];
                    }
                    else
                    {
                        if ((j * 25) + i - 1 < filteredInventories.Count)
                        {
                            obj[j * 3] = (j * 25) + i;
                            obj[(j * 3) + 1] = filteredInventories[(j * 25) + i - 1].NettWeight;
                            obj[(j * 3) + 2] = filteredInventories[(j * 25) + i - 1].GrossWeight;

                            summaryObj[(j * 3) + 1] = (decimal)summaryObj[(j * 3) + 1] + filteredInventories[(j * 25) + i - 1].NettWeight;
                            summaryObj[(j * 3) + 2] = (decimal)summaryObj[(j * 3) + 2] + filteredInventories[(j * 25) + i - 1].GrossWeight;
                        }
                    }
                }
                dt.Rows.Add(obj);
            }
        }
    }
}
