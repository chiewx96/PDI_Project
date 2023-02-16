using GalaSoft.MvvmLight;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using PDI_Feather_Tracking_WPF.Global;
using PDI_Feather_Tracking_WPF.Models;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        FeatherDbContext _dbContext;
        HomeView _homeView;
        SkuTypeSettingView _skuTypeSettingView;
        TareWeightView _tareWeightView;
        UserLevelView _userLevelView;
        UserView _userView;

        public MainViewModel(FeatherDbContext dbContext, HomeView homeView, SkuTypeSettingView skuTypeSettingView,
            TareWeightView tareWeightView, UserLevelView userLevelView, UserView userView)
        {
            MenuItems = new ObservableCollection<MenuItem>();
            #region Constructor Assigning
            _dbContext = dbContext;
            _homeView = homeView;
            _skuTypeSettingView = skuTypeSettingView;
            _tareWeightView = tareWeightView;
            _userLevelView = userLevelView;
            _userView = userView;
            #endregion

            foreach (var item in GenerateMenuItems().OrderBy(i => i.Name))
            {
                MenuItems.Add(item);
            }
            SelectedItem = MenuItems.First();
            _menuItemsView = CollectionViewSource.GetDefaultView(MenuItems);


            HomeCommand = new Command(
                _ =>
                {
                    SelectedIndex = 0;

                });
        }

        #region Private Methods

        private IEnumerable<MenuItem> GenerateMenuItems()
        {
            yield return new MenuItem(
               "Home",
               typeof(HomeView), _homeView);

            yield return new MenuItem(
                "Tare Weight",
                typeof(TareWeightView), _tareWeightView);

            yield return new MenuItem(
                "User",
                typeof(UserView), _userView);


            yield return new MenuItem(
                        "User Level",
                        typeof(UserLevelView), _userLevelView);


            yield return new MenuItem(
                    "Sku Type",
                    typeof(SkuTypeSettingView), _skuTypeSettingView);

        }
        #endregion


        #region Components
        private readonly ICollectionView _menuItemsView;

        public ObservableCollection<MenuItem> MenuItems { get; }

        private MenuItem? _selectedItem;
        public MenuItem? SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged(nameof(SelectedItem));
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => _selectedIndex = value;
        }

        public Command HomeCommand { get; }

        #endregion
    }
}
