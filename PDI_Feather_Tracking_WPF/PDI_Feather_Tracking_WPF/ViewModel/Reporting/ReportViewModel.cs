﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Helper;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class ReportViewModel : ViewModelBase
    {
        FeatherDbContext _dbContext;
        public ReportViewModel(FeatherDbContext dbContext)
        {
            _dbContext = dbContext;
            Messenger.Default.Register<SkuType>(this, _ => refresh_sku_types());
            ReportTypes = Enum.GetValues(typeof(ReportTypesEnum)).Cast<ReportTypesEnum>().ToList();
            SelectedReportType = ReportTypes.FirstOrDefault();
            refresh_sku_types();
            search(new SearchReportViewModel());
        }

        private void refresh_sku_types()
        {
            SkuTypes = _dbContext.SkuType.AsNoTracking().Where(x => x.Status).ToList();
        }

        private void search(object? obj)
        {
            if (obj is SearchReportViewModel vm)
            {
                List<InventoryRecords> temp_inventory_records = new List<InventoryRecords>();
                //temp_inventory_records = _dbContext.InventoryRecords.AsNoTracking().Where(x =>
                //    vm.SkuTypeId > 0 ? x.SkuTypeId == vm.SkuTypeId : x.SkuTypeId > 0 &&

                //    vm.OutgoingFromDate != null && vm.OutgoingToDate != null ? x.OutgoingDateTime >= 
                //    vm.OutgoingFromDate && x.OutgoingDateTime <= vm.OutgoingToDate :
                //    vm.OutgoingFromDate != null ? x.OutgoingDateTime >= vm.OutgoingFromDate : x.Id > 0 &&

                //    vm.IncomingFromDate != null && vm.IncomingToDate != null ? x.IncomingDateTime >= 
                //    vm.IncomingFromDate && x.IncomingDateTime <= vm.IncomingToDate :
                //    vm.IncomingFromDate != null ? x.IncomingDateTime >= vm.IncomingFromDate : x.Id > 0
                //).ToList();

                temp_inventory_records = _dbContext.InventoryRecords.AsNoTracking().ToList();
                if (vm.SkuTypeId > 0)
                    temp_inventory_records = temp_inventory_records.Where(x => x.SkuTypeId > 0).ToList();

                if (vm.OutgoingFromDate != null && vm.OutgoingToDate != null)
                    temp_inventory_records = temp_inventory_records.Where(x => x.OutgoingDateTime >= vm.OutgoingFromDate &&
                    x.OutgoingDateTime <= vm.OutgoingToDate).ToList();
                else if (vm.OutgoingFromDate != null)
                    temp_inventory_records = temp_inventory_records.Where(x => x.OutgoingDateTime >= vm.OutgoingFromDate).ToList();

                if (vm.IncomingFromDate != null && vm.IncomingToDate != null)
                    temp_inventory_records = temp_inventory_records.Where(x => x.IncomingDateTime >= vm.IncomingFromDate &&
                    x.IncomingDateTime <= vm.IncomingToDate).ToList();
                else if (vm.IncomingFromDate != null)
                    temp_inventory_records = temp_inventory_records.Where(x => x.IncomingDateTime >= vm.IncomingFromDate).ToList();

                temp_inventory_records.ForEach(x =>
                {
                    if (x.SkuType == null)
                        x.SkuType = SkuTypes.Where(z => z.Id == x.SkuTypeId).First();
                });
                FilteredInventories = temp_inventory_records;
            }
        }

        private void generateReport(object? obj)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            string path = "C:\\Users\\chiew\\OneDrive\\Documents\\BarTender";
            if (SelectedReportType != null)
            {
                Action action = null;
                switch (SelectedReportType)
                {
                    case ReportTypesEnum.IncomingReport:
                        action = () => ReportHelper.GenerateIncomingReport(FilteredInventories, path);
                        break;
                    case ReportTypesEnum.ActualWeightList:
                        action = () => ReportHelper.GenerateActualWeightList(FilteredInventories, path);
                        break;
                    default:
                        break;
                }

                if (action != null)
                {
                    try
                    {
                        Task.Run(action, cancellationTokenSource.Token).ContinueWith(_ => General.SendNotifcation("Report Generated Successfully"));
                        cancellationTokenSource.CancelAfter(5000);
                    }
                    catch (TaskCanceledException taskCancelledException)
                    {
                        // log
                        General.SendNotifcation("Report generate failure");
                    }
                }
            }
        }

        private List<SkuType> skuTypes;

        public List<SkuType> SkuTypes
        {
            get { return skuTypes; }
            set { skuTypes = value; RaisePropertyChanged(nameof(SkuTypes)); }
        }

        private List<ReportTypesEnum> reportTypes;

        public List<ReportTypesEnum> ReportTypes
        {
            get { return reportTypes; }
            set { reportTypes = value; RaisePropertyChanged(nameof(ReportTypes)); }
        }

        private ReportTypesEnum? selectedReportType;

        public ReportTypesEnum? SelectedReportType
        {
            get { return selectedReportType; }
            set { selectedReportType = value; RaisePropertyChanged(nameof(SelectedReportType)); }
        }

        private List<InventoryRecords> filteredInventories;

        public List<InventoryRecords> FilteredInventories
        {
            get { return filteredInventories; }
            private set { filteredInventories = value; RaisePropertyChanged(nameof(FilteredInventories)); }
        }
        public ICommand SearchCommand => new Command(search);
        public ICommand GenerateCommand => new Command(generateReport);
    }
}