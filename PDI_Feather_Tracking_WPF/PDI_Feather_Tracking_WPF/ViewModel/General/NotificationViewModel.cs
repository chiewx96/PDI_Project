using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class NotificationViewModel : ViewModelBase
    {

        public NotificationViewModel()
        {
            Messenger.Default.Register<NotificationViewModel>(this, m => { Message = m.Message; });
        }

        private async void handle_reset_notification()
        {
            int timeout = 2000;
            await Task.Delay(timeout);
            Message = string.Empty;
        }

        private string message = string.Empty;

        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                ShowNotification = value != string.Empty ? true : false;
                RaisePropertyChanged(nameof(Message));

            }
        }

        private bool showNotification = false;

        public bool ShowNotification
        {
            get { return showNotification; }
            set
            {
                showNotification = value;
                if (value)
                    handle_reset_notification();
                RaisePropertyChanged(nameof(ShowNotification));
            }
        }

    }
}
