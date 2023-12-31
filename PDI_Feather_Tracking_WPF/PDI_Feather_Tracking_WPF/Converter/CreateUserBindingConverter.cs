﻿using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PDI_Feather_Tracking_WPF.Converter
{
    class CreateUserBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            User user = new User();
            if (values[0].ToString() != string.Empty)
                user.EmployeeNo = values[0].ToString();
            if (values[1] != null && int.TryParse(values[1].ToString(), out int user_level))
                user.UserLevelId = user_level;
            if (user.EmployeeNo != null || user.UserLevelId > 0)
                return user;
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if(value is User vm)
            {
                var result = new object[] { vm.EmployeeNo, vm.UserLevelId };
                return result;
            }
            return null;
        }
    }
}
