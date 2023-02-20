using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class CreateUserViewModel : ViewModelBase
    {
        FeatherDbContext _featherDbContext;
        public CreateUserViewModel(FeatherDbContext featherDbContext)
        {
            _featherDbContext = featherDbContext;
            refresh_user_levels();
        }

        public void refresh_user_levels()
        {
            UserLevels =_featherDbContext.UserLevels.AsNoTracking().ToList();
        }

        private string empNo = string.Empty;

		public string EmpNo
		{
			get { return empNo; }
			set { empNo = value; }
		}

        private string selectedUserLevel = string.Empty;

        public string SelectedUserLevel
        {
            get { return selectedUserLevel; }
            set { selectedUserLevel = value; RaisePropertyChanged(nameof(SelectedUserLevel)); }
        }

        private List<UserLevel> userLevels = new List<UserLevel>();

        public List<UserLevel> UserLevels
        {
            get { return userLevels; }
            set { userLevels = value; RaisePropertyChanged(nameof(UserLevels)); }
        }
    }
}
