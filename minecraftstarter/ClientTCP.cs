using ResourceSaver;
using System.Net;
using System.Net.NetworkInformation;

namespace minecraftstarter
{
    internal class ClientTCP
    {
        internal bool isUsed = true;
        internal void CheckForPort()
        {
            int port = Settings.port; 
            isUsed = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    isUsed = true;
                    break;
                }
            }
        }
    }
}
