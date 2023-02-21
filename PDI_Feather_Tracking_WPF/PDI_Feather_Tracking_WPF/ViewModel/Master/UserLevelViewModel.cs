using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Model;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Markup;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class UserLevelViewModel : ViewModelBase
    {
        FeatherDbContext _dbContext;
        LoginViewModel _loginViewModel;
        ConfirmationViewModel _confirmationViewModel;
        Confirmation _confirmation;

        public UserLevelViewModel(FeatherDbContext dbContext, LoginViewModel loginViewModel, ConfirmationViewModel confirmationViewModel, Confirmation confirmation)
        {
            _dbContext = dbContext;
            _loginViewModel = loginViewModel;
            _confirmationViewModel = confirmationViewModel;
            _confirmation = confirmation;
            populate_user_rights();
            populate_all_modules();
            update_module_status_from_db();
            saveCommand = new Command(save_module_access);
            createCommand = new Command(create_new_user_level);
            deleteCommand = new Command(delete_user_level);
        }

        #region Private Methods
        private void populate_user_rights()
        {
            UserLevels = _dbContext.UserLevels.AsNoTracking().Where(x => x.Status).ToList();
            SelectedUserLevel = UserLevels.First();
            Messenger.Default.Send(UserLevels);
        }

        private void populate_all_modules()
        {
            var modules = _dbContext.Module.AsNoTracking().ToList();
            modules.ForEach(z =>
            {
                ModuleAccess.Add(new ModuleAccess { Module = z, Status = 1 });
            });
        }

        private void update_module_status_from_db()
        {
            string? module_access_from_db = _dbContext.UserLevels.AsNoTracking().Where(x => x.Id == SelectedUserLevel.Id).First().ModuleAccess;
            if (module_access_from_db != null)
            {
                ModuleAccess = JsonConvert.DeserializeObject<List<ModuleAccess>>(module_access_from_db);
            }
        }

        private void save_module_access(Object? obj)
        {
            var selected_user_level = _dbContext.UserLevels.Where(z => z.Id == SelectedUserLevel.Id).First();
            selected_user_level.ModuleAccess = JsonConvert.SerializeObject(ModuleAccess);
            selected_user_level.Name = SelectedUserLevel.Name;
            selected_user_level.CreatedBy = _loginViewModel.CurrentUser?.Id ?? 0;
            selected_user_level.UpdatedBy = _loginViewModel.CurrentUser?.Id ?? 0;
            selected_user_level.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();
            General.SendNotifcation("Saved");
            populate_user_rights();
        }

        private void create_new_user_level(object? obj)
        {
            var sample_access = _dbContext.UserLevels.AsNoTracking().Where(x => x.Id == SelectedUserLevel.Id).First().ModuleAccess;
            var user_lvl = new UserLevel()
            {
                ModuleAccess = sample_access,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                CreatedBy = _loginViewModel.CurrentUser?.Id ?? 0,
                UpdatedBy = _loginViewModel.CurrentUser?.Id ?? 0,
                Name = "New Role",
                Status = true
            };
            _dbContext.Add(user_lvl);
            _dbContext.SaveChanges();
            General.SendNotifcation("New user level Created");
            populate_user_rights();
        }

        private void delete_user_level(object? obj)
        {
            if (SelectedUserLevel != null)
            {
                _confirmationViewModel.set("Are you sure want to delete this user level?", confirm_delete_user_level);
                _confirmation.Show();
            }
        }

        private void confirm_delete_user_level()
        {
            var item = _dbContext.UserLevels.Where(x => x.Id == SelectedUserLevel.Id).FirstOrDefault();
            if (item != null)
            {
                item.Status = false;
                _dbContext.SaveChanges();
                General.SendNotifcation("Deleted");
            }
            populate_user_rights();
        }


        #endregion

        #region Components
        private List<UserLevel> _userLevel = new List<UserLevel>();

        public List<UserLevel> UserLevels
        {
            get { return _userLevel; }
            set
            {
                _userLevel = value;
                RaisePropertyChanged(nameof(UserLevels));
            }
        }

        private UserLevel _selectedUserLevel = new UserLevel();

        public UserLevel SelectedUserLevel
        {
            get { return _selectedUserLevel; }
            set
            {
                if (value != null)
                {
                    _selectedUserLevel = value;
                    update_module_status_from_db();
                    RaisePropertyChanged(nameof(SelectedUserLevel));
                }
            }
        }

        private List<ModuleAccess> _moduleAccess = new List<ModuleAccess>();

        public List<ModuleAccess> ModuleAccess
        {
            get { return _moduleAccess; }
            set { _moduleAccess = value; RaisePropertyChanged(nameof(ModuleAccess)); }
        }


        private Command saveCommand;

        public Command SaveCommand => saveCommand;


        private Command createCommand;

        public Command CreateCommand => createCommand;


        private Command deleteCommand;

        public Command DeleteCommand => deleteCommand;


        #endregion
    }
}
