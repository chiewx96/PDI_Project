using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Linq;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        TareWeightView _tareWeightView;
        FeatherDbContext _dbContext;
        public HomeViewModel(FeatherDbContext dbContext, TareWeightView tareWeightView)
        {
            Messenger.Default.Register<User?>(this,
                refresh_tare_weight_access);
            _tareWeightView = tareWeightView;
            _dbContext = dbContext;
            refresh_tare_weight_setting();
        }


        #region private methods
        private async void show_dialog(object? _)
        {
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
            refresh_tare_weight_setting();
        }

        private void refresh_tare_weight_setting()
        {
            try
            {
                TareWeightSetting = _dbContext.TareWeightSetting.First();
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

        #endregion

        #region Property
        public ICommand ModifyTareWeightCommand => new Command(show_dialog);

        private TareWeightSetting tareWeightSetting = new TareWeightSetting();

        public TareWeightSetting TareWeightSetting
        {
            get { return tareWeightSetting; }
            set { tareWeightSetting = value; RaisePropertyChanged(nameof(TareWeightSetting)); }
        }

        private bool tareWeightAccess = false;

        public bool TareWeightAccess
        {
            get { return tareWeightAccess; }
            set { tareWeightAccess = value; RaisePropertyChanged(nameof(TareWeightAccess)); }
        }
        #endregion
    }
}
