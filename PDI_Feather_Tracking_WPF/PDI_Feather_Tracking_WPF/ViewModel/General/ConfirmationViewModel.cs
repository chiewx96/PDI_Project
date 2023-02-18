using GalaSoft.MvvmLight;
using PDI_Feather_Tracking_WPF.Interfaces;
using PDI_Feather_Tracking_WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PDI_Feather_Tracking_WPF.ViewModel
{
    public class ConfirmationViewModel : ViewModelBase, ICloseWindows
    {
        Action _confirmAction;
        DispatcherTimer dispatcherTimer;

        public ConfirmationViewModel()
        {

        }

        public void set(string headerText, Action confirmAction)
        {
            _confirmAction = confirmAction;
            HeaderText = headerText;
            ConfirmCommand = new Command(perform_action);
            start_cancel_timer();
        }

        private void perform_action(object? obj)
        {
            _confirmAction?.Invoke();
            close_window(null);
        }

        private void start_cancel_timer()
        {
            var autoStartingActionCountdownStart = DateTime.Now;
            CancelCommand = new Command(close_window);

            dispatcherTimer = new DispatcherTimer(
               TimeSpan.FromMilliseconds(100),
               DispatcherPriority.Normal,
               new EventHandler((o, e) =>
               {
                   var totalDuration = autoStartingActionCountdownStart.AddSeconds(15).Ticks - autoStartingActionCountdownStart.Ticks;
                   var currentDuration = DateTime.Now.Ticks - autoStartingActionCountdownStart.Ticks;
                   var autoCountdownPercentComplete = 100.0 / totalDuration * currentDuration;
                   CancelButtonProgress = autoCountdownPercentComplete;

                   if (CancelButtonProgress >= 100)
                   {
                       CancelButtonProgress = 0;
                       close_window(null);
                   }
               }), Dispatcher.CurrentDispatcher);
        }

        private void close_window(object? obj)
        {
            CancelButtonProgress = 0;
            dispatcherTimer.Stop();
            Action?.Invoke();
        }

        private double _cancelButtonProgress;

        public double CancelButtonProgress
        {
            get => _cancelButtonProgress;
            set
            {
                _cancelButtonProgress = value;
                RaisePropertyChanged(nameof(CancelButtonProgress));
            }
        }
        private string _headerText;

        public string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                RaisePropertyChanged(nameof(HeaderText));
            }
        }

        private Command _confirmCommand;

        public Command ConfirmCommand
        {
            get { return _confirmCommand; }
            set
            {
                _confirmCommand = value;
                RaisePropertyChanged(nameof(ConfirmCommand));
            }
        }

        private Command _cancelCommand;

        public Command CancelCommand
        {
            get { return _cancelCommand; }
            set
            {
                _cancelCommand = value;
                RaisePropertyChanged(nameof(CancelCommand));
            }
        }

        public Action Action { get; set; }
    }
}
