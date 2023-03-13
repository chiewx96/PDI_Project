using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        TcpClientHelper _tcpClientHelper;
        SerialCommunicationHelper _serialCommunicationHelper;
        int _printService = 0;
        int _weightService = 0;

        public HomeViewModel(FeatherDbContext dbContext, TareWeightView tareWeightView, TareWeightViewModel tareWeightViewModel,
             Confirmation confirmation, ConfirmationViewModel confirmationViewModel, IConfiguration configuration)
        {
            Messenger.Default.Register<User?>(this, _ =>
            {
                CurrentUser = _;
                refresh_tare_weight_access(_);
            });
            Messenger.Default.Register<SkuType?>(this,
                refresh_sku_types);
            Messenger.Default.Register<SerialCommunicationHelper>(this, _ =>
            {
                _serialCommunicationHelper = _;
                setup_weighing_machine_connection();
            });
            Messenger.Default.Register<TcpClientHelper>(this, _ =>
            {
                _tcpClientHelper = _;
            });

            _tareWeightViewModel = tareWeightViewModel;
            _tareWeightView = tareWeightView;
            _dbContext = dbContext;
            _confirmation = confirmation;
            _confirmationViewModel = confirmationViewModel;
            refresh_tare_weight_setting();
            int.TryParse(configuration.GetSection("PrintService").Value, out _printService);
            int.TryParse(configuration.GetSection("WeightService").Value, out _weightService);
            if (_printService == 1)
                _tcpClientHelper = new TcpClientHelper(configuration);
            if (_weightService == 1)
                _serialCommunicationHelper = new SerialCommunicationHelper(configuration);
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

        private void refresh_tare_weight_access(User? obj)
        {
            TareWeightAccess = General.CheckAccessibility(obj, ModuleEnum.tare_weight_setting);
        }

        private void show_confirm_record(object? obj)
        {
            _confirmationViewModel.set("Are you sure want to record this?", confirm_save_record);
            _confirmation.ShowDialog();
        }

        private void confirm_save_record()
        {
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
            General.SendNotifcation("Record Saved");
            handle_print_label(batch_no);
        }

        private void handle_print_label(string label_no)
        {
            if (_tcpClientHelper != null)
            {
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    Task.Run(() =>
                    {
                        string response = _tcpClientHelper.SendData(label_no, decimal.Round(GrossWeight, 4));
                        log($"Print status for batch number [{label_no}] => {response}");
                    }, cancellationTokenSource.Token);
                    cancellationTokenSource.CancelAfter(5000);
                }
                catch (TaskCanceledException taskCancelledException)
                {
                    General.SendNotifcation("Timeout! Label failed to print.");
                    // Log down fails.
                    // Notify user failure
                }
                cancellationTokenSource.CancelAfter(10000);

            }
        }

        private void setup_weighing_machine_connection()
        {
            if (_serialCommunicationHelper != null)
            {
                _serialCommunicationHelper.Set(receive_weighing_machine_input, 500);
                _serialCommunicationHelper.Start();
            }
        }

        private void receive_weighing_machine_input(object sender, SerialDataReceivedEventArgs e)
        {
            Debug.WriteLine(sender);
            log(sender.ToString());
        }

        private void test_action(object? obj)
        {
            GrossWeight = Math.Round((decimal)General.RandomNumberBetween(250, 260), 4);
            General.SendNotifcation($"Number generated {GrossWeight}");
        }

        private void log(params string[] content)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (var i = 0; i < content.Length; i++)
            {
                stringBuilder.Append(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")).Append(" : ").Append(content[i]).Append(Environment.NewLine);
            }
            Logger += stringBuilder.ToString();
        }

        #endregion

        #region Property
        public ICommand ModifyTareWeightCommand => new Command(show_dialog);

        public ICommand RecordCommand => new Command(_ => confirm_save_record());

        public ICommand TestCommand => new Command(test_action);

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


        private string logger = string.Empty;

        public string Logger
        {
            get { return logger; }
            set { logger = value; RaisePropertyChanged(nameof(Logger)); }
        }

        #endregion
    }
}
