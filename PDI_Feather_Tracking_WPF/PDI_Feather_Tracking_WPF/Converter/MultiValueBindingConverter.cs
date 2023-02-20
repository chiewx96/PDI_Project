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
    class TareWeightSettingBindingConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (int.TryParse(values[0].ToString(), out int _childCount) && decimal.TryParse(values[1].ToString(), out decimal _weight))
            {
                return new TareWeightViewModel()
                {
                    ChildCount = _childCount,
                    TareWeight = _weight
                };
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            if(value is TareWeightViewModel vm)
            {
                var result = new object[] { vm.ChildCount, vm.TareWeight };
                return result;
            }
            throw new NotImplementedException();
        }
    }
}
