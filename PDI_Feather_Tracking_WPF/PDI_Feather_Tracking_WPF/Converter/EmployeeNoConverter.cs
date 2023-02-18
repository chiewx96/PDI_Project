using PDI_Feather_Tracking_WPF.Models;
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
    class EmployeeNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is LoginViewModel vm && vm.CurrentUser != null)
            {
                return vm.CurrentUser.EmployeeNo;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
