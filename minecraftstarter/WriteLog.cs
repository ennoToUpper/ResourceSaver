using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minecraftstarter
{
    internal static class WriteLog
    {
        static bool isSetup = false;
        static StreamWriter streamWriter = null;

        internal static void WriteLogMessage(string message)
        {
            if (!isSetup || streamWriter == null)
            {
                SetupStream();
            }

            message = DateTime.Now + ": " + message;

            streamWriter?.WriteLine(message);

            streamWriter.Flush();

            Console.WriteLine(message);
        }

        private static void SetupStream()
        {

            DateTime now = DateTime.Now;

            if(!Directory.Exists("Logs"))
            {
                Directory.CreateDirectory("Logs");
            }

            string path = "Logs/Minecraft-AutoStart-Log-" + now.Year + "-" + now.Month + "-" + now.Day + ".txt";

            if (File.Exists(path))
            {
                streamWriter = new StreamWriter(path, true);
            }
            else
            {
                streamWriter = new StreamWriter(path);
            }

            isSetup = true;
        }
    }
}
