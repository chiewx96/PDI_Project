using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace PDI_Feather_Tracking_Service.GeneralService
{
    internal class TcpService
    {
        private TcpListener server;

        private bool isServerEnabled = false;

        public TcpService(Func<object, string> action, Func<object, string> log_action, int port)
        {
            server = null;
            try
            {
                // Set the TcpListener on port 13000.
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
                    try
                    {
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            string request_text = $"Received <<< {data}";
                            log_action(request_text);


                            string response_text = action(data); // do something on data received.

                            // Send back a response.
                            byte[] msg = Encoding.ASCII.GetBytes(response_text);
                            stream.Write(msg, 0, msg.Length);
                            log_action($"Send >>> {response_text}");
                        }
                    }
                    catch (Exception e)
                    {
                        log_action(e.Message);
                    }

                    log_action("disconnected");

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
            isServerEnabled = false;
            server = null;
        }
    }
}
