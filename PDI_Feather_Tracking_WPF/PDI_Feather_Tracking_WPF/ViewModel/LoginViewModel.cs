﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Interfaces;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class LoginViewModel : ViewModelBase, ICloseWindows
    {
        FeatherDbContext _dbContext;

        public LoginViewModel(FeatherDbContext dbContext)
        {
            Messenger.Default.Register<string>(this, _ =>
            {
                if (_ == General.CloseWindow)
                    LogoutAction?.Invoke();
            });
            _dbContext = dbContext;
            LoginCommand = new Command(login_action);
            CloseCommand = new Command((e) => Action?.Invoke());
            LogoutAction = new Action(() => set_user(null));
        }

        #region PrivateMethods
        private void login_action(object? obj)
        {
            User? result = General.TryLogin(UserNameTextContent, PasswordTextContent, ref _dbContext);
            set_user(result);
            if (result == null)
            {
                ErrorLogin = true;
                RaisePropertyChanged(nameof(ErrorLogin));
            }
            else
            {
                PasswordTextContent = string.Empty;
                PasswordDisplayContent = string.Empty;
                Action?.Invoke(); // close login window.
            }

        }

        private void set_user(User? user)
        {
            if (user == null && CurrentUser != null)
                _dbContext.Users.Where(x => x.Username == CurrentUser.Username).First().IsSignedIn = false;
            else if (user != null && CurrentUser == null)
                _dbContext.Users.Where(x => x.Username == user.Username).First().IsSignedIn = true;
            else
                throw new InvalidOperationException();
            _dbContext.SaveChanges();
            _currentUser = user;
            RaisePropertyChanged(nameof(CurrentUser));
            Messenger.Default.Send<LoginViewModel>(this);
        }

        private void reset_error()
        {
            ErrorLogin = false;
            RaisePropertyChanged(nameof(ErrorLogin));
        }
        #endregion

        #region Property
        public Action LogoutAction { get; set; }

        public Action Action { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand CloseCommand { get; set; }

        private User? _currentUser;

        public User? CurrentUser
        {
            get { return _currentUser; }
        }

        private string _usernameTextContent = string.Empty;

        public string UserNameTextContent
        {
            get { return _usernameTextContent; }
            set
            {
                _usernameTextContent = value;
                reset_error();
                RaisePropertyChanged(nameof(UserNameTextContent));
                RaisePropertyChanged(nameof(CanLogin));
            }
        }

        private string _passwordTextContent = string.Empty;

        public string PasswordTextContent
        {
            get { return _passwordTextContent; }
            set
            {
                if (value.Length > _passwordTextContent.Length)
                {
                    _passwordTextContent += string.Join("", value.Skip(_passwordTextContent.Length).Take(1));
                }
                else
                {
                    _passwordTextContent = string.Join("", _passwordTextContent.Take(value.Length));
                }
                reset_error();
                RaisePropertyChanged(nameof(PasswordTextContent));
                RaisePropertyChanged(nameof(PasswordDisplayContent));
                RaisePropertyChanged(nameof(CanLogin));
            }
        }

        public string PasswordDisplayContent
        {
            get
            {
                return new String('*', PasswordTextContent.Length);
            }
            set
            {
                PasswordTextContent = value;
            }
        }

        public bool CanLogin
        {
            get
            {
                return UserNameTextContent != string.Empty && PasswordTextContent != string.Empty
                    && CurrentUser == null;
            }
        }

        public bool ErrorLogin { get; set; }


        #endregion

    }
}
