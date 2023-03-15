using PDI_Feather_Tracking_WPF.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PDI_Feather_Tracking_WPF.Converter
{
    internal class PicConvertDateDisplay : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is InventoryRecords vm)
            {
                return vm.OutgoingPic > 0 ? vm.OutgoingDateTime : "-";
            }
            else return "-";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
