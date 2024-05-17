namespace Omni_Airbus.Model.FIDS
{
    /// <summary>
    /// <c>FIDSController</c> manages the FIDSWebServer.
    /// </summary>
    public class FIDSController
    {
        public Utils.MySQL Display {get; set;}

        /// <summary>
        /// Creates an intance of FIDSController
        /// </summary>
        public FIDSController()
        {
            Display = new Utils.MySQL("CALL GetFlightDetails()");
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