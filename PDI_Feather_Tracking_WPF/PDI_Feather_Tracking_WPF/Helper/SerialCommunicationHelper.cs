using GalaSoft.MvvmLight.Messaging;
using Google.Protobuf;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.Helper
{
    public class SerialCommunicationHelper
    {
        SerialPort _serialPort;
        int settings_completed = 0;
        Thread readThread;
        Action<decimal> _callBackAction;
        Action<object> _logAction;
        CancellationTokenSource _cancellationTokenSource;
        int _delayInterval;

        public SerialCommunicationHelper(IConfiguration configuration, Action<decimal> action, int readtimeout, Action<object> log)
        {
            try
            {
                _callBackAction = action;
                _logAction = log;
                _delayInterval = readtimeout;
                var machine = configuration.GetSection("weighing_machine");
                var _list = machine.GetChildren().ToList();
                Parity parity = Parity.None;
                StopBits stopBits = StopBits.None;
                Handshake handshake = Handshake.None;
                Enum.TryParse<Parity>(_list.Where(x => x.Key.ToLower() == "parity").First().Value, out parity);
                Enum.TryParse<StopBits>(_list.Where(x => x.Key.ToLower() == "stopbits").First().Value, out stopBits);
                Enum.TryParse<Handshake>(_list.Where(x => x.Key.ToLower() == "handshake").First().Value, out handshake);
                if (machine != null &&
                    int.TryParse(_list.Where(x => x.Key.ToLower() == "baudrate").First().Value, out int baudrate) &&
                    int.TryParse(_list.Where(x => x.Key.ToLower() == "databits").First().Value, out int databits))
                {
                    NewPort(_list.Where(x => x.Key.ToLower() == "portname").First().Value, baudrate, parity, databits, stopBits, handshake);
                }
            }
            catch (Exception e)
            {
                _logAction($"Failed to connect to weighting machine. Exception : {e.Message}");
            }
        }

        private void NewPort(string portName, int baudrate, Parity parity, int databits, StopBits stopBits, Handshake handshake)
        {
            _serialPort = new SerialPort(portName, baudrate, parity, databits, stopBits);
            _serialPort.Handshake = handshake;
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            //Messenger.Default.Send(this);
            settings_completed = 1;
            Start();
        }

        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort sp = (SerialPort)sender;
                string data = sp.ReadLine();
                if (data.StartsWith("ST,GS,"))
                {
                    try
                    {
                        string weight = data.Split(',')[2];
                        if (decimal.TryParse(weight.Trim(), out decimal _weight))
                            _callBackAction(_weight < 0 ? 0 : _weight);
                    }
                    catch (Exception ex)
                    {
                        _logAction($"Read value failed. Exception : {ex.Message}");
                    }
                }
            }
            catch { }
        }

        private bool Start()
        {
            if (settings_completed > 0 && !_serialPort.IsOpen)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _serialPort.Open();
                //ThreadPool.QueueUserWorkItem(new WaitCallback(Read), _cancellationTokenSource.Token);
                Messenger.Default.Register<string>(this, _ =>
                {
                    if (_ == General.StopWeighting)
                        Stop();
                });
            }
            return _serialPort.IsOpen;
        }

        public bool Stop()
        {
            if (_serialPort.IsOpen)
            {
                _cancellationTokenSource.Cancel();
                _serialPort.Close();
            }
            return !_serialPort.IsOpen;
        }
    }
}
