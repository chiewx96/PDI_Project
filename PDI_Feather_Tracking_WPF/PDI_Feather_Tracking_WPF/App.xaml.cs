using Microsoft.Extensions.DependencyInjection;
using PDI_Feather_Tracking_WPF.Global;
using System;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PDI_Feather_Tracking_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        private string connectionString;
        public IConfiguration Configuration { get; private set; }

        public App()
        {
            ServiceCollection services = new ServiceCollection();

            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();
            connectionString = Configuration.GetConnectionString("PDIFeatherTracking")?.ToString() ?? string.Empty;

            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<FeatherDbContext>(options =>
            {
                options.UseMySQL(connectionString);
            });

            ServiceConfiguration.ConfigureService(ref services, Configuration);
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
            mainWindow.Closed += Window_Closed;
        }

        private void Window_Closed(object? sender, EventArgs e)
        {
            Messenger.Default.Send<string>(General.CloseWindow);
        }
    }
}
