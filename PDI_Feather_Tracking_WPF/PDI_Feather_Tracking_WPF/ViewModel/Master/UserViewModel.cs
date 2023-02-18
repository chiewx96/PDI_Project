using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X509;
using PDI_Feather_Tracking_WPF.Dto;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class UserViewModel : ViewModelBase
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
            refresh_user_list();
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
                Password = General.Encrypt("abc123"),
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                UpdatedBy = CurrentUser?.Id ?? 0,
                CreatedBy = CurrentUser?.Id ?? 0,
                Status = true
            });
            _dbContext.SaveChanges();
            refresh_user_list();
        }

        private void refresh_user_list(int? user_level = null, string? employee_no = null)
        {
            try
            {
                if (user_level != null && employee_no != null)
                    Users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1 && z.EmployeeNo == employee_no && z.UserLevelId == user_level).ToList();
                else if (user_level != null)
                    Users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1 && z.UserLevelId == user_level).ToList();
                else if (employee_no != null)
                    Users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1 && z.EmployeeNo == employee_no).ToList();
                else
                    Users = _dbContext.Users.AsNoTracking().Where(z => z.Status && z.Id > 1).ToList();
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
                refresh_user_list();
            }
        }

        private void filter_user_by_parameter(object? obj)
        {
            if (obj is UserFilterDTO vm)
            {
                refresh_user_list(vm.user_level, vm.empNo);
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
                selected.UpdatedBy = CurrentUser?.Id ?? 0;
                selected.UpdatedAt = DateTime.Now;
                _dbContext.SaveChanges();
            }
            refresh_user_list();
        }

        #endregion


        #region Property
        public ICommand CreateNewUserCommand => new Command(show_dialog);

        public ICommand DeleteCommand => new Command(delete_user);

        public ICommand FilterCommand => new Command(filter_user_by_parameter);

        public ICommand SaveCommand => new Command(save_user);

        private List<User> users = new List<User>();

        public List<User> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChanged(nameof(Users)); }
        }

        private User? currentUser;

        public User? CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; RaisePropertyChanged(nameof(CurrentUser)); }
        }


        #endregion
    }
}
