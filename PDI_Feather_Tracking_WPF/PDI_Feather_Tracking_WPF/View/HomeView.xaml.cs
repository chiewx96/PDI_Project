using Microsoft.Extensions.Configuration;
using PDI_Feather_Tracking_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

namespace PDI_Feather_Tracking_WPF.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        IConfiguration _configuration;
        public HomeView(HomeViewModel homeViewModel, IConfiguration configuration)
        {
            DataContext = homeViewModel;
            _configuration = configuration;
            InitializeComponent();
        }

        private void Logger_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox tb = (TextBox)sender;
                tb.ScrollToEnd();
            }
            catch { }
        }

        private void Outbound_Click(object sender, RoutedEventArgs e)
        {
            string? outbound_url = _configuration.GetSection("OutboundLink").Value;
            try
            {
                if (outbound_url == null)
                {
                    General.SendNotifcation("outbound url is not found");
                }
                else
                {
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = outbound_url,
                        UseShellExecute = true
                    });
                    //System.Diagnostics.Process.Start(outbound_url);
                }
            }
            catch (Exception ex)
            {
                General.SendNotifcation("outbound url process error");
            }
        }
    }
}
