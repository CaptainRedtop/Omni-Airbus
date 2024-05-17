using Microsoft.VisualBasic;
using Omni_Airbus.Model.FIDS;
using Omni_Airbus.Utils;

namespace Omni_Airbus
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FIDSController WebServer = new FIDSController();
            WebServer.StartWebServer();
        }
    }
}