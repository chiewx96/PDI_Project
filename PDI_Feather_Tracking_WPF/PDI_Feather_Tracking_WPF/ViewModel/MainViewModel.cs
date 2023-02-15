using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ObservableCollection<MenuItem> MenuItems { get; }

        private MenuItem? _selectedItem;
        public MenuItem? SelectedItem
        {
            get => _selectedItem;
            set => _selectedItem  = value;
        }
    }
}
