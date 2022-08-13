using ResourceSaver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minecraftstarter
{
    internal static class MinecraftStarter
    {
        internal static void StartServer()
        {
            WriteLog.WriteLogMessage("Starting Server");
            ProcessStartInfo processToRunInfo = new ProcessStartInfo();
            processToRunInfo.Arguments = ($"-Xmx{Settings.arguments} -Xms256m -jar {Settings.jarname}");
            processToRunInfo.CreateNoWindow = true;
            processToRunInfo.WorkingDirectory = Settings.directory;
            processToRunInfo.FileName = "java.exe";
            Process process = new Process();
            process.StartInfo = processToRunInfo;
            process.Start();
        }
    }
}
