using Microsoft.Extensions.DependencyInjection;
using PDI_Feather_Tracking_WPF.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;

namespace PDI_Feather_Tracking_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<FeatherDbContext>(options =>
            {
                options.UseMySQL("server=localhost;port=3306;database=feather-tracking;user=root;password=root");
            });

            ServiceConfiguration.ConfigureService(ref services);
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
