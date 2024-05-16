namespace Omni_Airbus.Model.FIDS
{
    /// <summary>
    /// <c>FIDSController</c> manages the FIDSWebServer.
    /// </summary>
    public class FIDSController
    {
        public FIDSDisplay Display {get; set;}

        /// <summary>
        /// Creates an intance of FIDSController
        /// </summary>
        public FIDSController()
        {
            Display = new FIDSDisplay();
        }

        /// <summary>
        /// Create and start a thread for the WebServer
        /// </summary>
        public void StartWebServer()
        {
            Thread webServerThread = new(() =>
            {
                FIDSWebServer webServer = new();
                webServer.Host();
            });
            webServerThread.Name = "FIDSWebServer";
            webServerThread.Start();
        }
    }
}
