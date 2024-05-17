using Omni_Airbus.Model.FIDS;

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