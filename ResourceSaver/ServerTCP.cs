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
                    Thread.Sleep(100);

                    data = "";

                    NetworkStream stream = client.GetStream();

                    int i;

                    string fullData = "";

                    bool mcClient = true;

                    while (stream.DataAvailable)
                    {
                        i = stream.Read(bytes, 0, bytes.Length);

                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        fullData += data;

                        data = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        stream.Write(msg, 0, msg.Length);

                        if ((data.Contains(Settings.address, StringComparison.CurrentCultureIgnoreCase) 
                            && !data.Contains("Host", StringComparison.CurrentCultureIgnoreCase)) 
                            || data.Contains(Settings.keyword, StringComparison.OrdinalIgnoreCase))
                        {
                            mcClient = !data.Contains(Settings.keyword, StringComparison.OrdinalIgnoreCase);
                            RunServer = true;
                        }
                    }

                    WriteLog.WriteLogMessage(fullData);

                    if (RunServer)
                    {
                        string valueDetected = mcClient ? "Minecraft-Client" : "Keyword";
                        WriteLog.WriteLogMessage(valueDetected + " recognized!");
                    }
                    else
                    {
                        WriteLog.WriteLogMessage("Connection has been rejected");
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
