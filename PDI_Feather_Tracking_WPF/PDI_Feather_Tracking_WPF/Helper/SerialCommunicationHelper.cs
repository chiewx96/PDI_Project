using GalaSoft.MvvmLight.Messaging;
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
        bool _readThreadStart = false;

        public SerialCommunicationHelper(IConfiguration configuration)
        {
            try
            {
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
                    new SerialCommunicationHelper(_list.Where(x => x.Key.ToLower() == "portname").First().Value, baudrate, parity, databits, stopBits, handshake);
                }
            }
            catch
            {
                // log : failed to connect to machine
            }
        }

        public SerialCommunicationHelper(string portName, int baudrate, Parity parity, int databits, StopBits stopBits, Handshake handshake)
        {
            _serialPort = new SerialPort(portName, baudrate, parity, databits, stopBits);
            _serialPort.Handshake = handshake;
            readThread = new Thread(Read);
            Messenger.Default.Send(this);
        }

        public void Set(SerialDataReceivedEventHandler eventHandler, int readtimeout)
        {
            _serialPort.DataReceived += new SerialDataReceivedEventHandler(eventHandler);
            _serialPort.ReadTimeout = readtimeout;
            settings_completed = 1;
        }

        public bool Start()
        {
            if (settings_completed > 0 && !_serialPort.IsOpen)
            {
                _serialPort.Open();
                _readThreadStart = true;
                readThread.Start();
            }
            return _serialPort.IsOpen;
        }

        public bool Stop()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                _readThreadStart = false;
            }
            return !_serialPort.IsOpen;
        }

        public void Read()
        {
            while (_readThreadStart)
            {
                try
                {
                    string message = _serialPort.ReadLine();
                    Debug.WriteLine(message);
                }
                catch (TimeoutException) { }
            }
        }



    }
}
