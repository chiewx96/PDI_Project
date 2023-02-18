using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class SkuTypeSettingViewModel : ViewModelBase
    {
        FeatherDbContext _dbContext;
        LoginViewModel _loginViewModel;
        ConfirmationViewModel _confirmationViewModel;
        Confirmation _confirmation;
        public SkuTypeSettingViewModel(FeatherDbContext dbContext, LoginViewModel loginViewModel,
             ConfirmationViewModel confirmationViewModel, Confirmation confirmation)
        {
            _dbContext = dbContext;
            _loginViewModel = loginViewModel;
            _confirmationViewModel = confirmationViewModel;
            _confirmation = confirmation;
            DeleteCommand = new Command(_ => delete_item(_));
            CreateCommand = new Command(_ => create_new_sku_type());
            SaveCommand = new Command(_ => save_all_items());
            RaisePropertyChanged(nameof(DeleteCommand));
            RaisePropertyChanged(nameof(CreateCommand));
            RaisePropertyChanged(nameof(SaveCommand));
            refresh_view();
        }

        #region PrivateMethods

        private void save_all_items()
        {
            foreach (var z in SkuTypeSettings)
            {
                var selected = _dbContext.SkuType.Where(x => x.Id == z.Id).First();
                if (selected.Code != z.Code || selected.Description != z.Description)
                {
                    selected.Code = z.Code;
                    selected.Description = z.Description;
                    selected.UpdatedBy = _loginViewModel.CurrentUser?.Id ?? 0;
                    selected.UpdatedAt = DateTime.Now;
                }

            }
            _dbContext.SaveChanges();
            refresh_view();
        }

        private void refresh_view()
        {
            SkuTypeSettings = _dbContext.SkuType.AsNoTracking().Where(z => z.Status).ToList();
        }

        private void create_new_sku_type()
        {
            _dbContext.SkuType.Add(new SkuType
            {
                Code = '-',
                Description = "Please enter description.",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = _loginViewModel.CurrentUser?.Id ?? 0,
                UpdatedBy = _loginViewModel.CurrentUser?.Id ?? 0,
                Status = true
            });
            _dbContext.SaveChanges();
            refresh_view();
        }

        private void delete_item(object? obj)
        {
            if (obj is SkuType vm)
            {
                _confirmationViewModel.set("Are you sure want to delete this Sku Type?", () => confirm_delete_item(vm));
                _confirmation.Show();
            }
        }

        private void confirm_delete_item(object? _)
        {
            if (_ is SkuType vm)
            {
                var target = _dbContext.SkuType.Where(z=>z.Id == vm.Id).First();
                target.Status = false;
                target.UpdatedBy = _loginViewModel.CurrentUser?.Id ?? 0;
                target.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
                refresh_view();
            }
        }

        #endregion

        #region Property
        private List<SkuType> _skuTypeSettings = new List<SkuType>();

        public List<SkuType> SkuTypeSettings
        {
            get { return _skuTypeSettings; }
            set { _skuTypeSettings = value; RaisePropertyChanged(nameof(SkuTypeSettings)); }
        }


        public ICommand DeleteCommand { get; private set; }

        public ICommand CreateCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        #endregion
    }
}
