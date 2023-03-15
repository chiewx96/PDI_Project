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
    internal class SearchReportConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                SearchReportViewModel vm = new SearchReportViewModel();

                if (values[1] != null)
                    vm.SkuTypeId = ((SkuType)values[1]).Id;

                if (values[2] != null)
                    vm.IncomingFromDate = (DateTime)values[2];

                if (values[3] != null)
                    vm.IncomingToDate = (DateTime)values[3];

                if (values[4] != null)
                    vm.OutgoingFromDate = (DateTime)values[4];

                if (values[5] != null)
                    vm.OutgoingToDate = (DateTime)values[5];

                return vm;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            try
            {
                if (value is SearchReportViewModel vm)
                {
                    return new object[] { vm.ReportTypeID, vm.SkuTypeId, vm.IncomingFromDate, vm.IncomingToDate, vm.OutgoingFromDate, vm.OutgoingToDate };
                }
                else throw new InvalidCastException();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }
    }
}
