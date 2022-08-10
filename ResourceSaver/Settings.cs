using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceSaver
{
    internal static class Settings
    {
        internal static int port = 0;
        internal static string arguments = "";
        internal static string jarname = "";
        internal static string directory = "";
        internal static string keyword = "";

        internal static void LoadSettings()
        {
            using (var stream = new StreamReader("settings.txt"))
            {
                while (!stream.EndOfStream)
                {
                    var lineSplit = stream.ReadLine()?.Split('>');

                    if (lineSplit?.Length > 1)
                    {
                        string clean = lineSplit[1].Replace(" ", "");
                        switch (lineSplit[0])
                        {
                            case "Port": port = int.Parse(clean); break;
                            case "Server RAM": arguments = clean; break;
                            case "Jar Name": jarname = clean; break;
                            case "Server Directory": directory = clean; break;
                            case " HTTP Keyword": keyword = clean; break;
                            default: break;
                        }
                    }
                }
            }
        }
    }
}
