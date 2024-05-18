namespace Omni_Airbus.Model.FIDS
{
    /// <summary>
    /// <c>FIDSItem</c> models a row in the FIDS display.
    /// </summary>
    public class FIDSItem
    {
        /// <summary>
        /// Create a new FIDS Item
        /// </summary>
        /// <param name="depature"></param>
        /// <param name="destination"></param>
        /// <param name="gateID"></param>
        /// <param name="airline"></param>
        public FIDSItem(DateTime depature, string destination, int gateID, string airline)
        {
            Departure = depature;
            Destination = destination;
            GateID = gateID;
            Airline = airline;
        }

        public DateTime Departure { get; private set; }
        public string Destination {get; private set; }
        public int? GateID { get; private set; }
        public string Airline { get; private set; }
    }
}
