using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PDI_Feather_Tracking_Service.GeneralService
{
    internal class TcpService
    {
        private TcpListener server;

        private bool isServerEnabled = false;

        public TcpService(Action<object> action)
        {
            server = null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);
                // Start listening for client requests.
                server.Start();
                isServerEnabled = true;

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (isServerEnabled)
                {
                    Debug.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    using TcpClient client = server.AcceptTcpClient();
                    Debug.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();
                    int i;
                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        action($"Received: {data}");

                        // Process the data sent by the client.
                        data = data.ToUpper();

                        string response_text = "success";
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(response_text);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Debug.WriteLine("Sent: {0}", data);
                    }
                }
            }
            catch (SocketException e)
            {
                Debug.WriteLine("SocketException: {0}", e);
                isServerEnabled = false;
            }
            finally
            {
                server.Stop();
            }

        }

        public void Dispose()
        {
            server.Stop();
            isServerEnabled= false;
            server = null;
        }
    }
}
