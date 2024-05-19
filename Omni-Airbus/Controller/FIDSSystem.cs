using Omni_Airbus.Model.FIDS;
using Omni_Airbus.Utils.Logging;

namespace Omni_Airbus.Controller
{
    /// <summary>
    /// <c>FIDSController</c> manages the FIDSWebServer.
    /// </summary>
    public class FIDSSystem
    {
        public FIDSDisplay Display { get; set; }
        internal Logger Log;

        /// <summary>
        /// Creates an intance of FIDSSytem.
        /// </summary>
        public FIDSSystem()
        {
            Display = new FIDSDisplay();
            Log = new Logger(LoggerEnum.Information);
            StartWebServer();
        }

        /// <summary>
        /// Create and start a thread for the WebServer
        /// </summary>
        public void StartWebServer()
        {
            Log.Information("Starting WebServer");
            Thread webServerThread = new(() =>
            {
                FIDSWebServer webServer = new();
                webServer.Host();
            });
            webServerThread.Name = "FIDSWebServer";
            webServerThread.Start();
            Log.Information($"Started: {webServerThread.Name}");
        }
    }
}