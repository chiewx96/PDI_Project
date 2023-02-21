using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;

namespace PDI_Feather_Tracking_WPF.Helper
{
    public class TcpClientHelper
    {
        private TcpClient client;
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private string _ip;
        private int _port;

        public TcpClientHelper(IConfiguration configuration)
        {
            client = new TcpClient();
            if (int.TryParse(configuration.GetSection("PrintServicePort").Value, out int port))
            {
                string ip = "127.0.0.1";
                _ip = ip;
                _port = port;
                client.Connect(ip, port);
                //AutoReconnectHandler(ip, port);
                Messenger.Default.Send(this);
            }
        }

        public string SendData(string content, decimal gross_weight, decimal tare_weight, decimal nett_weight, string employee_no, string date)
        {
            try
            {
                
                if (!client.Connected)
                {
                    client = new TcpClient();
                    client.Connect(_ip, _port);
                }

                if (client.Connected)
                {
                    using NetworkStream networkStream = client.GetStream();
                    networkStream.ReadTimeout = 2000;

                    //Read Response
                    using var reader = new StreamReader(networkStream, Encoding.UTF8);

                    var dict = new Dictionary<string, string>()
                    {
                        {"batch_no", content },
                        {"gross_weight", gross_weight.ToString() },
                        {"tare_weight", tare_weight.ToString() },
                        {"incoming_date", date },
                    };
                    byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dict));
                    networkStream.Write(bytes, 0, bytes.Length);

                    content = reader.ReadToEnd();
                    Debug.WriteLine($"Response: {reader.ReadToEnd()}");
                }
                else content = $"Client is not connected. Print fail for Batch No : {content}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally { client.Close(); }
            return content;
        }

        public void StopAutoReconnect()
        {
            cancellationTokenSource.Cancel();
        }

        private void AutoReconnectHandler(string ip, int port)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    while (!client.Connected)
                    {
                        try
                        {
                            client.Connect(ip, port);
                        }
                        catch (Exception ex)
                        {
                            var w32ex = ex as Win32Exception;
                            if (w32ex == null)
                            {
                                w32ex = ex.InnerException as Win32Exception;
                            }
                            if (w32ex != null)
                            {
                                int code = w32ex.ErrorCode;
                                if (code == 10056)
                                {
                                    Debug.WriteLine("Socket already connected by others");
                                    StopAutoReconnect();
                                }
                                Debug.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }, cancellationTokenSource.Token);
        }
    }
}
