using GalaSoft.MvvmLight.Messaging;
using Microsoft.Extensions.Configuration;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
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

        public TcpClientHelper(int port)
        {
            client = new TcpClient();
            _ip = "127.0.0.1";
            _port = port;
            client.Connect(_ip, port);
        }

        public string SendData(object data, Action<string> action_callback)
        {
            string responseStr = string.Empty;
            try
            {
                if (!client.Connected)
                {
                    client = new TcpClient();
                    client.Connect(_ip, _port);
                }

                if (client.Connected)
                {
                    //Send Request
                    using NetworkStream networkStream = client.GetStream();
                    networkStream.ReadTimeout = 6000;
                    byte[] bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data));
                    networkStream.Write(bytes, 0, bytes.Length);

                    //Read Response
                    byte[] buffer = new byte[1024];
                    int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    action_callback?.Invoke(response);
                }
                else responseStr = $"Client is not connected. Port:{_port}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally { client.Close(); }
            return responseStr;
        }
    }
}
