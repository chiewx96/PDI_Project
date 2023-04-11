using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Helper;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        TareWeightView _tareWeightView;
        TareWeightViewModel _tareWeightViewModel;
        FeatherDbContext _dbContext;
        Confirmation _confirmation;
        ConfirmationViewModel _confirmationViewModel;
        TcpClientHelper _printerTcpHelper;
        TcpClientHelper _loggerTcpHelper;
        SerialCommunicationHelper _serialCommunicationHelper;

        public HomeViewModel(FeatherDbContext dbContext, TareWeightView tareWeightView, TareWeightViewModel tareWeightViewModel,
             Confirmation confirmation, ConfirmationViewModel confirmationViewModel, IConfiguration configuration)
        {
#if !DEBUG
            start_service();
#endif
            Messenger.Default.Register<string>(this, _ =>
            {
                if (_ == General.RecordCommand)
                    key_down_action();
            });
            Messenger.Default.Register<User?>(this, _ =>
            {
                CurrentUser = _;
                refresh_user_access(_);
            });

            Messenger.Default.Register<SkuType?>(this,
                refresh_sku_types);
            Messenger.Default.Register<LoggerModel?>(this,
                _ => log(_.Message));
            if (int.TryParse(configuration.GetSection("LogServicePort").Value, out int logger_port))
                connect_logger_service(logger_port);
            _tareWeightViewModel = tareWeightViewModel;
            _tareWeightView = tareWeightView;
            _dbContext = dbContext;
            _confirmation = confirmation;
            _confirmationViewModel = confirmationViewModel;
            refresh_tare_weight_setting();
            int.TryParse(configuration.GetSection("PrintService").Value, out int _printService);
            int.TryParse(configuration.GetSection("WeightService").Value, out int _weightService);
            if (_printService == 1 && int.TryParse(configuration.GetSection("PrintServicePort").Value, out int port))
                connect_print_service(port);
            if (_weightService == 1)
                _serialCommunicationHelper = new SerialCommunicationHelper(configuration, receive_weighing_machine_input, 500, (a) => log(a.ToString()));
        }



        #region private methods
        private async void show_dialog(object? _)
        {
            _tareWeightViewModel.set(TareWeightSetting);
            var result = await DialogHost.Show(_tareWeightView, "RootDialog", null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is TareWeightViewModel vm)
                update_tare_weight_setting_to_database(vm);
        }

        private void update_tare_weight_setting_to_database(TareWeightViewModel tareWeightViewModel)
        {
            var item = _dbContext.TareWeightSetting.First();
            item.Weight = decimal.Round(tareWeightViewModel.TareWeight, 2);
            //item.UpdatedBy = 
            item.ChildCount = tareWeightViewModel.ChildCount;
            _dbContext.SaveChanges();
            General.SendNotifcation("Tare Weight setting updated");
            refresh_tare_weight_setting();
        }

        private void refresh_tare_weight_setting()
        {
            try
            {
                TareWeightSetting = _dbContext.TareWeightSetting.AsNoTracking().First();
            }
            catch (Exception ex)
            {
                // write log
            }
        }

        private void refresh_sku_types(SkuType? skuType)
        {
            try
            {
                SkuTypes = _dbContext.SkuType.AsNoTracking().Where(x => x.Status).ToList();
                if (SelectedSkuType == null)
                    SelectedSkuType = SkuTypes.FirstOrDefault();
            }
            catch (Exception ex)
            {
                // write log
            }
        }

        private void refresh_user_access(User? obj)
        {
            TareWeightAccess = General.CheckAccessibility(obj, ModuleEnum.tare_weight_setting);
            CanOutbound = General.CheckAccessibility(obj, ModuleEnum.outgoing);
        }

        private void show_confirm_record(object? obj)
        {
            _confirmationViewModel.set("Are you sure want to record this?", confirm_save_record);
            _confirmation.ShowDialog();
        }

        private void confirm_save_record()
        {
            if (SelectedSkuType == null || SelectedSkuType.Id == null || SelectedSkuType.Id == 0) return;
            string batch_no = General.GenerateRunningNumber(SelectedSkuType.Code, SelectedSkuType.LastSkuCode, Math.Truncate(GrossWeight));
            _dbContext.InventoryRecords.Add(new InventoryRecords
            {
                BatchNo = batch_no,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IncomingDateTime = DateTime.Now,
                SkuTypeId = SelectedSkuType.Id,
                GrossWeight = GrossWeight,
                TareWeight = TareWeight,
                NettWeight = NettWeight,
                CreatedBy = CurrentUser?.Id ?? 0,
                UpdatedBy = CurrentUser?.Id ?? 0,
            });

            // update this batch no as latest batch no into skutype table.
            _dbContext.SkuType.Where(x => x.Id == SelectedSkuType.Id).First().LastSkuCode = batch_no;
            SelectedSkuType.LastSkuCode = batch_no;
            _dbContext.SaveChanges();
            handle_saved_records(batch_no);
            log("Record Saved");
            handle_print_label(batch_no, GrossWeight, SelectedSkuType.Description);
        }

        private void handle_saved_records(string? batch_no)
        {
            string content = string.Empty;
            if (batch_no == null)
            {
                content = $"Batch No | Gross Weight | Nett Weight | Tare Weight | Incoming DateTime | SKU Type ";
            }
            else
            {
                InventoryRecords? inventory = _dbContext.InventoryRecords.AsNoTracking().Where(x => x.BatchNo == batch_no).FirstOrDefault();
                if (inventory != null)
                {
                    inventory.SkuType = _dbContext.SkuType.AsNoTracking().Where(x => x.Id == inventory.SkuTypeId).FirstOrDefault();
                    content = $"Batch number : {inventory.BatchNo} | Gross Weight : {inventory.GrossWeight} | Nett Weight : {inventory.NettWeight} | " +
                        $"Tare Weight : {inventory.TareWeight} | Incoming : {inventory.IncomingDateTime} | Sku Type : {inventory.SkuType.Code}";
                }
            }
            SavedRecords += content;
            SavedRecords += Environment.NewLine;
        }

        private void handle_print_label(string label_no, decimal grossWeight, string? title)
        {
            if (title == null)
            {
                log("SKU type description is not properly set");
                return;
            }
            if (_printerTcpHelper != null)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    Task.Run(async () =>
                    {
                        log($"Start printing for batch number [{label_no}]");
                        var dict = new Dictionary<string, string>()
                        {
                            {"title", title},
                            {"batch_no", label_no },
                            {"gross_weight", decimal.Round(grossWeight, 4).ToString() },
                        };
                        string response = _printerTcpHelper.SendData(dict, handlePrinterResponse);
                        //log($"Print status for batch number [{label_no}] => {response}");
                    }, cancellationTokenSource.Token);
                    cancellationTokenSource.CancelAfter(5000);
                }
                catch (TaskCanceledException taskCancelledException)
                {
                    log("Timeout! Label failed to print.");
                    // Log down fails.
                    // Notify user failure
                }
                cancellationTokenSource.CancelAfter(10000);
            }
        }

        private void receive_weighing_machine_input(decimal data)
        {
            //Debug.WriteLine($"Data read : {data}");
            //log(data.ToString());
            GrossWeight = data;
        }

        private void test_action(object? obj)
        {
            //GrossWeight = Math.Round((decimal)General.RandomNumberBetween(250, 260), 4);
            //General.SendNotifcation($"Number generated {GrossWeight}");
            log("test_logger");
        }

        private void log(params string[] content)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (var i = 0; i < content.Length; i++)
            {
                stringBuilder.Append(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")).Append(" : ").Append(content[i]).Append(Environment.NewLine);
            }
            Logger += stringBuilder.ToString();
            if (_loggerTcpHelper != null)
            {
                Task.Run(() => _loggerTcpHelper.SendData(stringBuilder.ToString(), null));
            }
        }

        private void handlePrinterResponse(string response)
        {
            switch (response.ToLower())
            {
                case "failure":
                    log($"Label fail to print.");
                    break;
                case "success":
                    log($"Label printed successfully.");
                    break;
            }
        }

        private void key_down_action()
        {
            if (SelectedSkuType == null || SelectedSkuType.Id <= 0)
                log("Sku Type is not set");
            else
                confirm_save_record();
        }

        private void start_service()
        {
            try
            {

                ServiceHelper.RunService("PDI_Feather_Service");
            }
            catch (Exception ex)
            {
                General.SendNotifcation("Service cannot be started.");
            }
        }

        private void connect_logger_service(int logger_port)
        {
            try
            {
                _loggerTcpHelper = new TcpClientHelper(logger_port);
            }
            catch
            {
                General.SendNotifcation("logger service cannot be connected.");
            }
        }

        private void connect_print_service(int port)
        {
            try
            {
                _printerTcpHelper = new TcpClientHelper(port);
            }
            catch
            {
                General.SendNotifcation("logger service cannot be connected.");
            }
        }

        private void reprint(object? obj)
        {
            log($"Start to reprint {ReprintBatchNo}");
            var record = _dbContext.InventoryRecords.AsNoTracking().Where(z => z.BatchNo == ReprintBatchNo.Trim()).FirstOrDefault();
            if (string.IsNullOrEmpty(ReprintBatchNo))
                log("reprint batch number cannot be empty.");
            else if (record == null)
                log("Batch number to reprint is not found.");
            else
            {
                string title = _dbContext.SkuType.Where(x => x.Id == record.SkuTypeId).First().Description;
                handle_print_label(record.BatchNo, record.GrossWeight, title);
            }
        }

        #endregion

        #region Property
        public ICommand ModifyTareWeightCommand => new Command(show_dialog);

        public ICommand RecordCommand => new Command(_ => confirm_save_record());

        public ICommand TestCommand => new Command(test_action);

        public ICommand ReprintCommand => new Command(reprint);

        private User? currentUser;

        public User? CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(nameof(CurrentUser)); }
        }

        private List<SkuType> skuTypes = new List<SkuType>();

        public List<SkuType> SkuTypes
        {
            get { return skuTypes; }
            set { skuTypes = value; RaisePropertyChanged(nameof(SkuTypes)); }
        }

        private SkuType selectedSkuType = new SkuType();

        public SkuType SelectedSkuType
        {
            get { return selectedSkuType; }
            set { selectedSkuType = value; RaisePropertyChanged(nameof(SelectedSkuType)); }
        }

        private TareWeightSetting tareWeightSetting = new TareWeightSetting();

        public TareWeightSetting TareWeightSetting
        {
            get { return tareWeightSetting; }
            set
            {
                tareWeightSetting = value;
                RaisePropertyChanged(nameof(TareWeightSetting));
                RaisePropertyChanged(nameof(TareWeight));
                RaisePropertyChanged(nameof(NettWeight));
            }
        }

        private bool tareWeightAccess = false;

        public bool TareWeightAccess
        {
            get { return tareWeightAccess; }
            set { tareWeightAccess = value; RaisePropertyChanged(nameof(TareWeightAccess)); }
        }

        private decimal grossWeight;

        public decimal GrossWeight
        {
            get { return grossWeight; }
            set
            {
                grossWeight = value;
                RaisePropertyChanged(nameof(GrossWeight));
                RaisePropertyChanged(nameof(NettWeight));
            }
        }

        public decimal TareWeight => TareWeightSetting != null ? TareWeightSetting.ChildCount * TareWeightSetting.Weight : 0;


        public decimal NettWeight => GrossWeight - TareWeight < 0 ? 0 : GrossWeight - TareWeight;

        private string reprintBatchNo;

        public string ReprintBatchNo
        {
            get { return reprintBatchNo; }
            set { reprintBatchNo = value; RaisePropertyChanged(nameof(ReprintBatchNo)); }
        }

        private string logger = string.Empty;

        public string Logger
        {
            get { return logger; }
            set { logger = value; RaisePropertyChanged(nameof(Logger)); }
        }

        private string savedRecords = string.Empty;

        public string SavedRecords
        {
            get { return savedRecords; }
            set { savedRecords = value; RaisePropertyChanged(nameof(SavedRecords)); }
        }

        private bool canOutBound = false;

        public bool CanOutbound
        {
            get { return canOutBound; }
            private set { canOutBound = value; RaisePropertyChanged(nameof(CanOutbound)); }
        }

        #endregion
    }
}
