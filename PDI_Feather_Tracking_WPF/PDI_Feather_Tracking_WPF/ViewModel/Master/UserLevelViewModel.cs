using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Model;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class UserLevelViewModel : ViewModelBase
    {
        FeatherDbContext _dbContext;

        public UserLevelViewModel(FeatherDbContext dbContext)
        {
            _dbContext = dbContext;
            populate_user_rights();
            populate_all_modules();
            update_module_status_from_db();
        }


        #region Private Methods
        private void populate_user_rights()
        {
            UserLevels = _dbContext.UserLevels.AsNoTracking().ToList();
            SelectedUserLevel = UserLevels.First().Id;
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
            string? module_access_from_db = _dbContext.UserLevels.Where(x => x.Id == SelectedUserLevel).First().ModuleAccess;
            if (module_access_from_db != null)
            {
                ModuleAccess = JsonConvert.DeserializeObject<List<ModuleAccess>>(module_access_from_db);
            }
        }

        private void save_module_access()
        {
            var selected_user_level = _dbContext.UserLevels.Where(z => z.Id == SelectedUserLevel).First();
            selected_user_level.ModuleAccess = JsonConvert.SerializeObject(ModuleAccess);
            //selected_user_level.UpdatedBy();
            selected_user_level.UpdatedAt = DateTime.Now;
            _dbContext.SaveChanges();
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

        private List<ModuleAccess> _moduleAccess = new List<ModuleAccess>();

        public List<ModuleAccess> ModuleAccess
        {
            get { return _moduleAccess; }
            set { _moduleAccess = value; RaisePropertyChanged(nameof(SelectedUserLevel)); }
        }


        private int selectedUserLevel;

        public int SelectedUserLevel
        {
            get { return selectedUserLevel; }
            set
            {
                selectedUserLevel = value;
                update_module_status_from_db();
                RaisePropertyChanged(nameof(SelectedUserLevel));
            }
        }


        #endregion
    }
}
