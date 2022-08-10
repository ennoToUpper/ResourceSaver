// See https://aka.ms/new-console-template for more information
using minecraftstarter;
using ResourceSaver;

Console.WriteLine("Started Minecraft-Server-Listener");

var ServerTCP = new ServerTCP();
var ClientTCP = new ClientTCP();

Settings.LoadSettings();

while (true)
{

    ClientTCP.CheckForPort();
    WriteLog.WriteLogMessage("Port available: " + !ClientTCP.isUsed);

    if (!ClientTCP.isUsed)
    {
        ServerTCP.StartServer();
        Thread.Sleep(2000);

        if (ServerTCP.RunServer)
        {
            MinecraftStarter.StartServer();
            ServerTCP.RunServer = false;

            WriteLog.WriteLogMessage("Port checking starts in 20s");

        }
    }

    Thread.Sleep(20000);
}