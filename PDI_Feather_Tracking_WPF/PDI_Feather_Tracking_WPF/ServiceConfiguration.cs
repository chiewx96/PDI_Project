using Microsoft.Extensions.DependencyInjection;
using PDI_Feather_Tracking_WPF.View;
using PDI_Feather_Tracking_WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF
{
    public static class ServiceConfiguration
    {
        public static void ConfigureService(ref Microsoft.Extensions.DependencyInjection.ServiceCollection services)
        {

            services.AddSingleton<TareWeightView>();
            services.AddSingleton<TareWeightViewModel>();

            services.AddSingleton<SkuTypeSettingView>();
            services.AddSingleton<SkuTypeSettingViewModel>();

            services.AddSingleton<UserLevelView>();
            services.AddSingleton<UserLevelViewModel>();

            services.AddSingleton<UserView>();
            services.AddSingleton<UserViewModel>();

            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<HomeView>();


            services.AddSingleton<Confirmation>();
            services.AddSingleton<ConfirmationViewModel>();

            services.AddSingleton<LoginView>();
            services.AddSingleton<LoginViewModel>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<MainWindow>();
        }
    }
}
