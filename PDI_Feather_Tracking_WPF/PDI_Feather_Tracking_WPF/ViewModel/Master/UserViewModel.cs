using GalaSoft.MvvmLight;
using PDI_Feather_Tracking_WPF.Models;
using System;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class UserViewModel : ViewModelBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; RaisePropertyChanged(nameof(Id)); }
        }

        private string _username;

        public string Username
        {
            get { return _username; }
            set { _username = value; RaisePropertyChanged(nameof(Username)); }
        }


        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; RaisePropertyChanged(nameof(Password)); }
        }

        private string _employeeNo;

        public string EmployeeNo
        {
            get { return _employeeNo; }
            set { _employeeNo = value; RaisePropertyChanged(nameof(EmployeeNo)); }
        }

        public int UserLevelId { get; set; }

        public bool Status { get; set; }

        public bool IsSignedIn { get; set; }

        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public UserLevel UserLevel { get; set; }

        public UserViewModel(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
            EmployeeNo = user.EmployeeNo;
            UserLevelId = user.UserLevelId;
            Status = user.Status;
            IsSignedIn = user.IsSignedIn;
            CreatedBy = user.CreatedBy;
            UpdatedBy = user.UpdatedBy;
            CreatedAt = user.CreatedAt;
            UpdatedAt = user.UpdatedAt;
            UserLevel = user.UserLevel;
        }

        public User ConvertToUser()
        {
            return new User
            {
                Id = Id,
                Username = Username,
                Password = Password,
                EmployeeNo = EmployeeNo,
                UserLevelId = UserLevelId,
                Status = Status,
                IsSignedIn = IsSignedIn,
                CreatedBy = CreatedBy,
                UpdatedBy = UpdatedBy,
                CreatedAt = CreatedAt,
                UpdatedAt = UpdatedAt
            };
        }
    }
}
