using ResourceSaver;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace minecraftstarter
{
    internal class ServerTCP
    {
        internal bool RunServer = false;
        internal void StartServer()
        {
            TcpListener? server = null;

            try
            {               
                int port = Settings.port;

                server = new TcpListener(IPAddress.Any, port);

                server.Start();

                Byte[] bytes = new Byte[256];
                string data = "";

                while (true)
                {
                    WriteLog.WriteLogMessage("TCP Open - Waiting for Connection");

                    TcpClient client = server.AcceptTcpClient();
                    WriteLog.WriteLogMessage("Connected!");

                    data = "";

                    NetworkStream stream = client.GetStream();

                    int i;

                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);

                        if(data.Contains("\u0001\0") || data.Contains(Settings.keyword, StringComparison.OrdinalIgnoreCase))
                        {
                            string valueDetected = data.Contains('\u0001') ? "Minecraft-Client" : "Keyword";

                            WriteLog.WriteLogMessage(valueDetected + " recognized!");
                            RunServer = true;
                            break;
                        }
                    }

                    break;
                }
            }
            catch (SocketException e)
            {
                WriteLog.WriteLogMessage("SocketException: " + e.Message);
            }
            finally
            {
                server?.Stop();
            }
        }
    }
}
