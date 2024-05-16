namespace Omni_Airbus.Model.FIDS
{
    public class FIDSItem
    {
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
