using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Interfaces;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class UserViewModel : ViewModelBase, IAction
    {
        FeatherDbContext _dbContext;
        CreateUserView _createUserView;
        CreateUserViewModel _createUserViewModel;
        ConfirmationViewModel _confirmationViewModel;
        Confirmation _confirmation;
        public UserViewModel(FeatherDbContext dbContext, CreateUserView createUserView, CreateUserViewModel createUserViewModel,
            LoginViewModel loginViewModel, ConfirmationViewModel confirmationViewModel, Confirmation confirmation)
        {
            _dbContext = dbContext;
            _createUserView = createUserView;
            _createUserViewModel = createUserViewModel;
            _confirmationViewModel = confirmationViewModel;
            _confirmation = confirmation;
            Messenger.Default.Register<User?>(this,
                refresh_current_user);
            Messenger.Default.Register<List<UserLevel>>(this, refresh_user_levels);
            refresh_user_list();
            refresh_user_levels();
        }

        private void refresh_user_levels(List<UserLevel> obj = null)
        {
            if (obj != null)
                UserLevels = obj;
            else
                UserLevels = _dbContext.UserLevels.AsNoTracking().Where(x => x.Status).ToList();
        }


        #region private methods
        private void refresh_current_user(User? obj)
        {
            CurrentUser = obj;
        }

        private async void show_dialog(object? _)
        {
            _createUserViewModel.refresh_user_levels();
            var result = await DialogHost.Show(_createUserView, "RootDialog", null, ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is User newUser)
                create_new_user(newUser);
        }

        private void create_new_user(User newUser)
        {
            _dbContext.Users.Add(new User
            {
                EmployeeNo = newUser.EmployeeNo,
                Username = newUser.EmployeeNo,
                UserLevelId = newUser.UserLevelId,
                Password = EncryptionHelper.Encrypt("abc123"),
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedBy = CurrentUser?.Id ?? 0,
                CreatedBy = CurrentUser?.Id ?? 0,
                Status = true
            });
            _dbContext.SaveChanges();
            General.SendNotifcation("New user created");
            refresh_user_list();
        }

        private void refresh_user_list(string? employee_no = null, int? user_level = null)
        {
            try
            {
                if (user_level != null && employee_no != null)
                    users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1 && z.EmployeeNo.StartsWith(employee_no) && z.UserLevelId == user_level).ToList();
                else if (user_level != null)
                    users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1 && z.UserLevelId == user_level).ToList();
                else if (employee_no != null)
                    users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1 && z.EmployeeNo.StartsWith(employee_no)).ToList();
                else
                    users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1).ToList();

                users.ForEach(x =>
                {
                    x.UserLevel = _dbContext.UserLevels.AsNoTracking().Where(z => z.Status && z.Id == x.UserLevelId).First();
                });
                RaisePropertyChanged(nameof(Users));
            }
            catch (Exception ex)
            {
                // write log
            }
        }

        private void delete_user(object? obj)
        {
            if (obj is User vm)
            {
                _confirmationViewModel.set("Are you sure want to delete this user?", () => confirm_delete_item(vm));
                _confirmation.Show();
            }
        }

        private void confirm_delete_item(object? _)
        {
            if (_ is User vm)
            {
                var target = _dbContext.Users.Where(z => z.Id == vm.Id).First();
                target.Status = false;
                target.UpdatedBy = CurrentUser?.Id ?? 0;
                target.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
                General.SendNotifcation("Deleted");
                refresh_user_list();
            }
        }

        private void filter_user_by_parameter(object? obj)
        {
            if (obj is User vm)
            {
                refresh_user_list(vm.EmployeeNo, vm.UserLevelId);
            }
            else refresh_user_list();

        }

        private void save_user(object? obj)
        {
            if (obj is User user)
            {
                var selected = _dbContext.Users.Where(x => x.Id == user.Id).First();
                selected.EmployeeNo = user.EmployeeNo;
                selected.Username = user.EmployeeNo;
                selected.UserLevelId = user.UserLevelId;
                selected.UpdatedBy = CurrentUser?.Id ?? 0;
                selected.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
                General.SendNotifcation("Saved");
            }
            refresh_user_list();
        }

        private void clear_filter(object? obj)
        {
            refresh_user_list();
            Action?.Invoke();
        }

        private void reset_password(object? obj)
        {
            if (obj is User vm)
            {
                _confirmationViewModel.set($"Are you sure want to reset user {vm.EmployeeNo}'s password?", () => confirm_reset_password(vm));
                _confirmation.Show();
            }
        }

        private void confirm_reset_password(User user)
        {

            var selected = _dbContext.Users.Where(x => x.Id == user.Id).First();
            selected.Password = EncryptionHelper.Encrypt("abc123");
            selected.UpdatedBy = CurrentUser?.Id ?? 0;
            selected.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();
            General.SendNotifcation("Password has been reset");
            RefreshCommand.Execute(null);
        }
        #endregion


        #region Property
        public ICommand CreateNewUserCommand => new Command(show_dialog);

        public ICommand DeleteCommand => new Command(delete_user);

        public ICommand FilterCommand => new Command(filter_user_by_parameter);

        public ICommand ResetCommand => new Command(reset_password);

        public ICommand SaveCommand => new Command(save_user);

        public ICommand RefreshCommand => new Command(clear_filter);

        private List<User> users = new List<User>();

        public ObservableCollection<User> Users
        {
            get { return new ObservableCollection<User>(users); }
            set { users = value.ToList(); RaisePropertyChanged(nameof(Users)); }
        }

        private User? currentUser;

        public User? CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(nameof(CurrentUser)); }
        }

        private List<UserLevel> userLevels;

        public List<UserLevel> UserLevels
        {
            get { return userLevels; }
            set { userLevels = value; RaisePropertyChanged(nameof(UserLevels)); }
        }

        public Action Action { get; set; }


        #endregion
    }
}
