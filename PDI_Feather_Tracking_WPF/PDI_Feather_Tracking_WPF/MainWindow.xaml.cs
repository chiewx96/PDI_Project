using PDI_Feather_Tracking_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
        {


        }

        private void MenuDarkModeButton_Click(object sender, RoutedEventArgs e)
        {


        }

        private void FlowDirectionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnSelectedItemChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
