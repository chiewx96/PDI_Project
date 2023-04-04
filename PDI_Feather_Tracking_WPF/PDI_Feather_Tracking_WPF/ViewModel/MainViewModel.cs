using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Helper;
using PDI_Feather_Tracking_WPF.Interfaces;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        FeatherDbContext _dbContext;
        HomeView _homeView;
        SkuTypeSettingView _skuTypeSettingView;
        UserLevelView _userLevelView;
        UserView _userView;
        LoginViewModel _loginViewModel;
        LoginView _loginView;
        ReportView _reportView;

        public MainViewModel(FeatherDbContext dbContext, HomeView homeView, SkuTypeSettingView skuTypeSettingView,
            UserLevelView userLevelView, UserView userView, LoginViewModel loginViewModel, LoginView loginView, ReportView reportView)
        {
            Messenger.Default.Register<User?>(this, _ =>
            {
                CurrentUser = _;
            });

            Messenger.Default.Register<string>(this, _ =>
            {
                if (_ == General.RefreshUserAccess)
                    SelectedItem = MenuItems.Where(x => x.IsVisible).FirstOrDefault();
            });


            MenuItems = new ObservableCollection<MenuItem>();
            #region Constructor Assigning
            _dbContext = dbContext;
            _homeView = homeView;
            _skuTypeSettingView = skuTypeSettingView;
            _userLevelView = userLevelView;
            _userView = userView;
            _loginViewModel = loginViewModel;
            _loginView = loginView;
            _reportView = reportView;
            #endregion
            _showLogin = new Command(_ => show_login());
            _changePassword = new Command(_ => ChangePasswordMode = true);
            _saveChangedPassword = new Command(_ => save_changed_password());
            _logout = new Command(_ =>
            {
                Messenger.Default.Send<User>(null);
            });

            foreach (var item in GenerateMenuItems().OrderBy(i => i.Name))
            {
                MenuItems.Add(item);
            }
            show_login();
        }



        #region Private Methods

        private IEnumerable<MenuItem> GenerateMenuItems()
        {
            yield return new MenuItem(
               "Home",
               typeof(HomeView), _homeView, ModuleEnum.incoming);

            yield return new MenuItem(
                "User",
                typeof(UserView), _userView, ModuleEnum.user);


            yield return new MenuItem(
                        "User Level",
                        typeof(UserLevelView), _userLevelView, ModuleEnum.user_level);


            yield return new MenuItem(
                    "Sku Type",
                    typeof(SkuTypeSettingView), _skuTypeSettingView, ModuleEnum.sku_type);


            yield return new MenuItem(
                    "Report",
                    typeof(ReportView), _reportView, ModuleEnum.reporting);

        }

        private void show_login()
        {
            if (_loginView != null && _currentUser == null)
            {
                _loginView.ShowDialog();
                _loginView.Focus();
            }
        }

        private void save_changed_password()
        {
            try
            {
                if (NewPassword != string.Empty)
                {
                    var current_user = _dbContext.Users.Where(x => x.Id == CurrentUser.Id).First();
                    current_user.Password = EncryptionHelper.Encrypt(NewPassword);
                    current_user.UpdatedAt = DateTime.Now;
                    current_user.UpdatedBy = CurrentUser.Id;
                    _dbContext.SaveChanges();
                    General.SendNotifcation("Password changed success");
                    NewPassword = string.Empty;
                    ChangePasswordMode = false;
                }
                else
                {
                    Message = "Please enter a valid password";
                    reset_message(20);
                }
            }
            catch (Exception ex)
            {
                // log
            }
        }

        private async void reset_message(int seconds)
        {
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            Message = string.Empty;
        }

        #endregion


        #region Components

        public ObservableCollection<MenuItem> MenuItems { get; }

        private MenuItem? _selectedItem;
        public MenuItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = value;
        }

        private bool changePasswordMode = false;

        public bool ChangePasswordMode
        {
            get { return changePasswordMode; }
            set { changePasswordMode = value; RaisePropertyChanged(nameof(ChangePasswordMode)); }
        }

        private string newPassword;

        public string NewPassword
        {
            get { return newPassword; }
            set { newPassword = value; RaisePropertyChanged(nameof(NewPassword)); }
        }

        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; RaisePropertyChanged(nameof(Message)); }
        }

        private User? _currentUser;
        public User? CurrentUser
        {
            get { return _currentUser; }
            private set
            {
                _currentUser = value;
                RaisePropertyChanged(nameof(CurrentUser));
            }
        }

        private ICommand _showLogin;

        public ICommand ShowLogin => _showLogin;


        private ICommand _changePassword;

        public ICommand ChangePassword => _changePassword;

        private ICommand _saveChangedPassword;

        public ICommand SaveChangedPassword => _saveChangedPassword;


        private ICommand _logout;

        public ICommand Logout => _logout;

        #endregion
    }
}
