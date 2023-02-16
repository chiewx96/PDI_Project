using PDI_Feather_Tracking_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PDI_Feather_Tracking_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel _mainViewModel;

        public MainViewModel MainViewModel
        {
            get { return _mainViewModel; }
        }


        public MainWindow(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            DataContext = _mainViewModel;
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;

            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void FlowDirectionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
            => MainScrollViewer.ScrollToHome();
    }
}
