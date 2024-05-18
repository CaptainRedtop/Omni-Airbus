using Omni_Airbus.Utils.Logger;

namespace Omni_Airbus.Model.FIDS
{
    /// <summary>
    /// <c>FIDSController</c> manages the FIDSWebServer.
    /// </summary>
    public class FIDSController
    {
        public FIDSDisplay Display {get; set;}
        internal Logger Log;

        /// <summary>
        /// Creates an intance of FIDSController
        /// </summary>
        public FIDSController()
        {
            Display = new FIDSDisplay();
            Log = new Logger(LoggerEnum.Information);
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